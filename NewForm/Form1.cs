using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LibraryFormGenerator;

namespace NewForm
{
    public partial class Form1 : Form
    {
        FormGenerator<Student> formGenerator;

        public Form1()
        {
            InitializeComponent();

            formGenerator = new FormGenerator<Student>();
            formGenerator.ShowDialog(new Student());
        }
    }
}
