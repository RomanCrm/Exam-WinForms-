using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace LibraryFormGenerator
{
    enum DecrementCoord
    {
        Plus = 120,
        Cx = 10,
        Cy = 20,
        PlusY = 30
    }

    public class FormGenerator<T>
    {
        Form form;

        private int x = (int)DecrementCoord.Cx;
        public int X
        {
            get => x;
        }
        private int y = (int)DecrementCoord.Cy;
        public int Y
        {
            get => y;
            set
            {
                y = value;
            }
        }

        int countOfStrInEnum = 0;

        public void ShowDialog(T argumentClass)
        {
            Type type = argumentClass.GetType();
            if (!type.IsClass)
            {
                MessageBox.Show("Объект не является классом");
            }
            else
            {
                form = new Form();
                form.Text = type.Name + "Form";

                foreach (PropertyInfo property in type.GetRuntimeProperties())
                {
                    CheckProp(property, X, Y);
                    Y += (int)DecrementCoord.PlusY;
                }

                AddButtons();

                form.ShowDialog();
            }
        }

        private void AddButtons()
        {
            Button cancelButton = new Button();
            cancelButton.Text = "Отмена";
            cancelButton.Location = new Point(X, Y);
            form.Controls.Add(cancelButton);
            Button okButton = new Button();
            okButton.Text = "Ок";
            okButton.Location = new Point(okButton.Size.Width + (int)DecrementCoord.Cx, Y);
            form.Controls.Add(okButton);
        }

        private void CheckProp(PropertyInfo property, int x, int y)
        {
            if (countOfStrInEnum != 0)
            {
                countOfStrInEnum = 0;
            }

            Label label = new Label();
            label.Location = new Point(x, y);
            label.Text = property.Name;

            form.Controls.Add(label);

            switch (property.PropertyType.Name)
            {
                case "String":
                    {
                        TextBox textBox = new TextBox();
                        textBox.Location = new Point(label.Location.X + (int)DecrementCoord.Plus, label.Location.Y);
                        form.Controls.Add(textBox);
                        break;
                    }
                case "Int16":
                case "UInt16":
                case "Int32":
                case "UInt32":
                case "Int64":
                case "UInt64":
                    {
                        NumericUpDown numeric = new NumericUpDown();
                        numeric.Location = new Point(label.Location.X + (int)DecrementCoord.Plus, label.Location.Y);
                        form.Controls.Add(numeric);
                        break;
                    }
                case "Boolean":
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox.Location = new Point(label.Location.X + (int)DecrementCoord.Plus, label.Location.Y);
                        form.Controls.Add(checkBox);
                        break;
                    }
                case "List`1":
                    {
                        ComboBox comboBox = new ComboBox();
                        comboBox.Location = new Point(label.Location.X + (int)DecrementCoord.Plus, label.Location.Y);
                        form.Controls.Add(comboBox);
                        break;
                    }
                case "Color":
                    {
                        Button button = new Button();
                        button.Text = "ColorDialog";
                        button.Click += ButtonColorDialogClick;
                        button.Location = new Point(label.Location.X + (int)DecrementCoord.Plus, label.Location.Y);
                        form.Controls.Add(button);
                        break;
                    }
                case "DateTime":
                    {
                        DateTimePicker dateTimePicker = new DateTimePicker();
                        dateTimePicker.Location = new Point(label.Location.X + (int)DecrementCoord.Plus,
                                                            label.Location.Y);
                        form.Controls.Add(dateTimePicker);
                        break;
                    }
                default:
                    {
                        if (property.PropertyType.IsEnum)
                        {
                            string[] namesEnum = property.PropertyType.GetEnumNames();

                            countOfStrInEnum = namesEnum.Length;

                            int yForRadio = 0;
                            foreach (string name in namesEnum)
                            {
                                RadioButton radioButton = new RadioButton();
                                radioButton.Text = name;
                                radioButton.Location = new Point(label.Location.X + (int)DecrementCoord.Plus,
                                                                 label.Location.Y + yForRadio);
                                form.Controls.Add(radioButton);
                                yForRadio += (int)DecrementCoord.PlusY;
                            }
                            Y = form.Controls[form.Controls.Count - 1].Location.Y;
                        }
                        break;
                    }
            }

            CheckSizeForm(label);

        }

        private void CheckSizeForm(Label label)
        {
            if ((label.Size.Width + form.Controls[form.Controls.Count - 1].Size.Width) >= form.Size.Width)
            {
                form.Size = new Size(label.Size.Width + form.Controls[form.Controls.Count - 1].Size.Width +
                                     (int)DecrementCoord.Plus, form.Size.Height);
            }
            if ((label.Location.Y + form.Controls[form.Controls.Count - 1].Size.Height) >= form.Size.Height ||
                 (label.Location.Y + form.Controls[form.Controls.Count - 1].Size.Height) < form.Size.Height - 10)
            {
                form.Size = new Size(form.Size.Width, label.Location.Y +
                                 form.Controls[form.Controls.Count - 1].Size.Height + (int)DecrementCoord.Plus);
            }
        }

        private void ButtonColorDialogClick(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
        }

    }

}
