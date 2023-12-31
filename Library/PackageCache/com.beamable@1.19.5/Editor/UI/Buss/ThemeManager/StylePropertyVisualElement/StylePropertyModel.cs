﻿using Beamable.Api.Autogenerated.Models;
using Beamable.Common;
using Beamable.Editor.Common;
using Beamable.Editor.UI.Validation;
using Beamable.UI.Buss;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Beamable.Editor.UI.Components
{
	public class StylePropertyModel
	{
		private static readonly DropdownEntry InitialOption =
			new DropdownEntry(Constants.Features.Buss.MenuItems.INITIAL_VALUE, false);

		private readonly Action<string> _removePropertyAction;
		private readonly Action _globalRefresh;
		private readonly Action<string, BussPropertyValueType> _setValueTypeAction;

		public BussStyleSheet StyleSheet { get; }
		public BussStyleRule StyleRule { get; }
		public BussPropertyProvider PropertyProvider { get; }
		public PropertySourceTracker PropertySourceTracker { get; }
		public BussElement AppliedToElement { get; }
		public BussElement InlineStyleOwner { get; }
		public string Tooltip { get; }
		public int VariableDropdownOptionIndex => GetOptionIndex();
		public List<DropdownEntry> DropdownOptions => GetDropdownOptions();

		public bool IsVariable => PropertyProvider.IsVariable;
		public bool IsInStyle => IsInline || (StyleRule != null && StyleRule.Properties.Contains(PropertyProvider));
		public bool IsWritable => IsInline || (StyleSheet != null && StyleSheet.IsWritable);
		private bool IsInline => InlineStyleOwner != null;

		public bool IsInherited => PropertyProvider.ValueType == BussPropertyValueType.Inherited;
		public bool IsInitial => PropertyProvider.ValueType == BussPropertyValueType.Initial;
		public bool HasVariableConnected => PropertyProvider.HasVariableReference;

		public bool IsVariableConnectionEmpty =>
			(PropertyProvider.GetProperty() is VariableProperty r) && string.IsNullOrEmpty(r.VariableName);
		public bool HasNonValueConnection => HasVariableConnected || IsInherited || IsInitial;

		public bool IsOverriden
		{
			get
			{
				if (PropertySourceTracker == null) return false;
				var appliedProvider = PropertySourceTracker.GetUsedPropertyProvider(PropertyProvider.Key, out var rank);
				if (PropertyProvider.ValueType == BussPropertyValueType.Inherited)
				{
					// I must be the first inherited property...
					var firstProvider = PropertySourceTracker.GetAllSources(PropertyProvider.Key).FirstOrDefault();
					return firstProvider?.PropertyProvider != PropertyProvider;
				}

				return appliedProvider != PropertyProvider;
			}
		}

		public StylePropertyModel(BussStyleSheet styleSheet,
								  BussStyleRule styleRule,
								  BussPropertyProvider propertyProvider,
								  PropertySourceTracker propertySourceTracker,
								  BussElement appliedToElement,
								  BussElement inlineStyleOwner,
								  Action<string> removePropertyAction,
								  Action globalRefresh,
								  Action<string, BussPropertyValueType> setValueTypeAction)
		{
			_removePropertyAction = removePropertyAction;
			_globalRefresh = globalRefresh;
			_setValueTypeAction = setValueTypeAction;
			StyleSheet = styleSheet;
			StyleRule = styleRule;
			PropertyProvider = propertyProvider;
			PropertySourceTracker = propertySourceTracker;
			AppliedToElement = appliedToElement;
			InlineStyleOwner = inlineStyleOwner;

			if (IsOverriden && IsInStyle && PropertySourceTracker != null)
			{
				PropertyReference reference =
					PropertySourceTracker.GetUsedPropertyReference(PropertyProvider.Key);

				Tooltip = reference.StyleRule != null
					? $"Property is overriden by {reference.StyleRule.SelectorString} rule from {reference.StyleSheet.name} stylesheet"
					: "Property is overriden by inline style";
			}
			else
			{
				Tooltip = String.Empty;
			}
		}


		public void LabelClicked(MouseDownEvent evt)
		{

			if (StyleSheet != null && !StyleSheet.IsWritable)
			{
				return;
			}

			List<GenericMenuCommand> commands = new List<GenericMenuCommand>
			{
				new GenericMenuCommand(Constants.Features.Buss.MenuItems.REMOVE, () =>
				{
					_removePropertyAction?.Invoke(PropertyProvider.Key);
				})
			};

			GenericMenu context = new GenericMenu();

			foreach (GenericMenuCommand command in commands)
			{
				GUIContent label = new GUIContent(command.Name);
				context.AddItem(new GUIContent(label), false, () => { command.Invoke(); });
			}

			context.ShowAsContext();
		}

		public void OnButtonClick(MouseDownEvent mouseDownEvent)
		{
			Undo.RecordObject(StyleSheet, "Use keyword");
			if (StyleRule.TryGetCachedProperty(PropertyProvider.Key, out var property))
			{
				PropertyProvider.SetProperty(property);
				StyleRule.RemoveCachedProperty(PropertyProvider.Key);
			}
			else
			{
				StyleRule.CacheProperty(PropertyProvider.Key, PropertyProvider.GetProperty());
				PropertyProvider.SetProperty(new VariableProperty());
			}

			if (StyleSheet != null)
			{
				StyleSheet.TriggerChange();
			}

			AssetDatabase.SaveAssets();
			_globalRefresh?.Invoke();
		}

		public void OnVariableSelected(int index)
		{
			var option = DropdownOptions[index];

			if (option.DisplayName == Constants.Features.Buss.MenuItems.INHERITED_VALUE)
			{
				Undo.RecordObject(StyleSheet, "Set inherited");
				PropertyProvider.GetProperty().ValueType = BussPropertyValueType.Inherited;
			}
			else if (option == InitialOption)
			{
				Undo.RecordObject(StyleSheet, "Set initial");
				PropertyProvider.GetProperty().ValueType = BussPropertyValueType.Initial;
			}
			else
			{
				Undo.RecordObject(StyleSheet, "Set variable");
				PropertyProvider.GetProperty().ValueType = BussPropertyValueType.Value;
				((VariableProperty)PropertyProvider.GetProperty()).VariableName = option.DisplayName;
			}

			if (StyleSheet != null)
			{
				StyleSheet.TriggerChange();
			}

			AssetDatabase.SaveAssets();
			_globalRefresh?.Invoke();
		}

		private List<DropdownEntry> GetDropdownOptions()
		{
			var options = new List<DropdownEntry> { InitialOption };
			var inheritedOption = new DropdownEntry(Constants.Features.Buss.MenuItems.INHERITED_VALUE, false);
			options.Add(inheritedOption);

			var baseType = BussStyle.GetBaseType(PropertyProvider.Key);
			if (PropertySourceTracker != null)
			{
				var variables = PropertySourceTracker.GetAllVariableNames(baseType).ToList();
				if (variables.Count > 0)
				{
					inheritedOption.LineBelow = true;
				}
				options.AddRange(variables);
			}

			return options;
		}

		private int GetOptionIndex()
		{
			var options = GetDropdownOptions();

			if (PropertyProvider.ValueType == BussPropertyValueType.Initial)
			{
				return 0;
			}
			if (PropertyProvider.ValueType == BussPropertyValueType.Inherited)
			{
				return 1;
			}

			string variableName = string.Empty;
			if (HasVariableConnected)
			{
				variableName = ((VariableProperty)PropertyProvider.GetProperty()).VariableName;
			}

			var value = options.FindIndex(option => option.DisplayName.Equals(variableName));
			value = Mathf.Clamp(value, 0, options.Count - 1);
			return value;
		}

		public void OnPropertyChanged(IBussProperty property)
		{
			if (StyleRule != null)
			{
				if (!StyleRule.HasProperty(PropertyProvider.Key))
				{
					StyleRule.TryAddProperty(PropertyProvider.Key, property);
				}
				else
				{
					StyleRule.GetPropertyProvider(PropertyProvider.Key).SetProperty(property);
				}
			}

			if (StyleSheet != null)
			{
#if UNITY_EDITOR
				StyleSheet.TrySetDirty();
#endif
				StyleSheet.TriggerChange();
			}

			AssetDatabase.SaveAssets();

			if (InlineStyleOwner != null)
			{
				InlineStyleOwner.Reenable();
			}
		}
	}
}
