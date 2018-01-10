using System;
using System.Collections.Generic;
using System.Drawing;

namespace NewForm
{
    public enum Numbers
    {
        One = 1,
        Two = 2,
        Three = 3
    }

    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsStudent { get; set; }
        public List<string> List { get; set; }
        public Numbers Numbers { get; set; }
        public Color Color { get; set; }
        public DateTime DateTime { get; set; }
    }
}