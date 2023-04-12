using DevExpress.Utils;
using DevExpress.XtraEditors;

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using Quicker.UI.DevExpress.Components.Inputs.Controls;
using Quicker.UI.DevExpress.Components.Inputs;
using Quicker.UI.DevExpress.Helpers;
using Quicker.UI.DevExpress.Shared.Enums;
using Quicker.Helpers;

namespace Quicker.Ui.DevExpressEx.Extensions
{
    public static partial class DevExpressExtensions
    {
        public static bool IsInDesignMode(this Control control)
        {
            return ResolveDesignMode(control);
        }

        /// <summary>
        /// Method to test if the control or it's parent is in design mode
        /// </summary>
        /// <param name="control">Control to examine</param>
        /// <returns>True if in design mode, otherwise false</returns>
        private static bool ResolveDesignMode(System.Windows.Forms.Control control)
        {
            System.Reflection.PropertyInfo designModeProperty;
            bool designMode;

            // Get the protected property
            designModeProperty = control.GetType().GetProperty("DesignMode", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            // Get the controls DesignMode value
            designMode = (bool)designModeProperty.GetValue(control, null);

            // Test the parent if it exists
            if (control.Parent != null)
            {
                designMode |= ResolveDesignMode(control.Parent);
            }

            return designMode;
        }
        public static void DataBindingsFrom(this XtraForm form, object records)
        {
            DataBindingsRemove(form);
            QuickerDevExpressHelper.ProcessAllControls(form,
               control =>
               {
                   var property = string.Empty;
                   var propertyIsBind = true;
                   if (control is BaseEdit)
                   {
                      // property = control.DataBindings.
                   }
                   if (control is QuickerEditor editor)
                   {
                       property = editor.Settings.DataBindings.Property;
                   }
                   if (control is PictureEdit picture)
                   {
                       property = picture.Tag?.ToString();
                       if (picture.Tag == null)                       
                           propertyIsBind=false;
                       
                       
                   }
                   
                   if (string.IsNullOrEmpty(property)) return;
                   try
                   {
                       if (propertyIsBind) 
                           control.DataBindings.Add("EditValue", records, property);
                   }
                   catch(Exception ex)
                   {
                       Trace.WriteLine("Erro {0}",ex.Message);
                   }
                
               });

        }

        public static void DataBindingsRemove(this XtraForm form)
        {
                QuickerDevExpressHelper.ProcessAllControls(form,
                control =>
                {
                    if (control is BaseEdit)
                    {
                        control.ResetText();
                        control.DataBindings.Clear();
                    }
                });
        }
        public static void DataBindingsRemove(this BaseEdit control)
        {
            control.ResetText();
            control.DataBindings.Clear();
        }
        public static void DataBindingsFrom(this BaseEdit control,object dataSource,string property)
        {
            control.DataBindingsRemove();
            control.DataBindings.Add("EditValue", dataSource, property);
        }
        public static void AddImages(this XtraForm form,QuickerEditor editor, ImageCollection imageCollection)
        {
            var xtedit = editor.GetEditor<QuickerComboEditImage>();
            xtedit.GetImages(imageCollection);
        }
        public static void InitData(this XtraForm form)
        {
            QuickerDevExpressHelper.ProcessAllControls(form,
            control =>
            {
                if (control is QuickerEditor)
                {
                    if (((QuickerEditor)control).Properties.Editor == EditorType.ComboEditImage)
                    {
                        var entity = ((QuickerEditor)control).Settings.DataBindings.Entity;
                        var propName = ((QuickerEditor)control).Settings.DataBindings.Property;

                        if (string.IsNullOrEmpty(entity) || string.IsNullOrEmpty(propName)) return;

                        //create entity
                        var instance = QuickerReflection.CreateInstance(entity);
                        //busca property;
                        var property = instance.GetType().GetProperty(propName);    //.GetProperties().SingleOrDefault(field => field.Name.Equals(propName));
                        if (property != null)
                        {
                            ForeignKeyAttribute attr = property.GetCustomAttributes(true).OfType<ForeignKeyAttribute>().SingleOrDefault();
                            if (attr != null) // busca lista na base de dados
                            {

                            }
                            else
                            {
                                if (property.PropertyType.IsEnum) // the source is enum
                                {

                                   // var data = EnumHelper.GetItems(property.PropertyType);
                                    QuickerDevExpressHelper.AddEnumToCombo((QuickerEditor)control, property.PropertyType);

                                }

                            }
                        }
                    }
                }
            });
        }

        /*
         private static void FormAddBind(Control controls, object dataSource)
         {
             foreach (Control ctrl in controls.Controls)
             {
                 switch (ctrl)
                 {
                     case OrionTextEdit orionTextEdit:
                         var dotIdx = orionTextEdit.DataFieldFromProperty.IndexOf(".", StringComparison.Ordinal);
                         var propValue = dataSource.GetValueFromProperty(orionTextEdit.DataFieldFromProperty);

                         // if (propValue == null && dotIdx == -1)
                         // {
                         //     dataSource.SetPropertyValue(orionTextEdit.DataFieldFromProperty, "");
                         // }

                         orionTextEdit.DataBindings.Add("EditValue", dataSource, orionTextEdit.DataFieldFromProperty);

                         break;
                     case OrionComboLookUp orionCombo:
                         orionCombo.DataBindings.Add("EditValue", dataSource, orionCombo.DataFieldFromProperty);
                         break;
                     case OrionComboBoxImage orionCombo:
                         orionCombo.DataBindings.Add("EditValue", dataSource, orionCombo.DataFieldFromProperty);
                         break;
                 }
                 FormAddBind(ctrl, dataSource);
             }
         }
         */
    }
}

