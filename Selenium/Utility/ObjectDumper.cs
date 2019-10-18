using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Selenium.Utility
{
    public class ObjectDumper
    {
        private readonly int depth;
        private int level;
        private int pos;

        private StringBuilder stringBuilder;

        private ObjectDumper(int depth) => this.depth = depth;

        public static string Write(object element) => Write(element, 0);


        public static string Write(object element, int depth)
        {
            var dumper = new ObjectDumper(depth) {stringBuilder = new StringBuilder()};
            dumper.WriteObject(null, element);
            return dumper.stringBuilder.ToString();
        }

        private void Write(string s)
        {
            if (s == null) return;
            stringBuilder.Append(s);
            pos += s.Length;
        }

        private void WriteIndent()
        {
            for (var i = 0; i < level; i++) stringBuilder.Append("  ");
        }

        private void WriteLine()
        {
            stringBuilder.AppendLine();
            pos = 0;
        }

        private void WriteTab()
        {
            Write("  ");
            while (pos % 8 != 0) Write(" ");
        }

        private void WriteObject(string prefix, object element)
        {
            if (element == null || element is ValueType || element is string)
            {
                WriteIndent();
                Write(prefix);
                WriteValue(element);
                WriteLine();
            }
            else
            {
                if (element is IEnumerable enumerableElement)
                {
                    foreach (object item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            WriteIndent();
                            Write(prefix);
                            Write("...");
                            WriteLine();
                            if (level < depth)
                            {
                                level++;
                                WriteObject(prefix, item);
                                level--;
                            }
                        }
                        else
                        {
                            WriteObject(prefix, item);
                        }
                    }
                }
                else
                {
                    var members =
                        element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    WriteIndent();
                    Write(prefix);
                    var propWritten = false;
                    foreach (MemberInfo memberInfo in members)
                    {
                        var fieldInfo = memberInfo as FieldInfo;
                        var propertyInfo = memberInfo as PropertyInfo;
                        if (fieldInfo != null || propertyInfo != null)
                        {
                            if (propWritten)
                                WriteTab();
                            else
                                propWritten = true;

                            Write(memberInfo.Name);
                            Write("=");
                            Type type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                            if (type.IsValueType || type == typeof(string))
                            {
                                WriteValue(fieldInfo != null
                                    ? fieldInfo.GetValue(element)
                                    : propertyInfo.GetValue(element, null));
                            }
                            else
                            {
                                if (typeof(IEnumerable).IsAssignableFrom(type))
                                    Write("...");
                                else
                                    Write("{ }");
                            }
                        }
                    }

                    if (propWritten) WriteLine();
                    if (level < depth)
                    {
                        foreach (MemberInfo memberInfo in members)
                        {
                            var fieldInfo = memberInfo as FieldInfo;
                            var propertyInfo = memberInfo as PropertyInfo;
                            if (fieldInfo != null || propertyInfo != null)
                            {
                                Type type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                                if (!(type.IsValueType || type == typeof(string)))
                                {
                                    object value = fieldInfo != null
                                        ? fieldInfo.GetValue(element)
                                        : propertyInfo.GetValue(element, null);
                                    if (value != null)
                                    {
                                        level++;
                                        WriteObject($"{memberInfo.Name}: ", value);
                                        level--;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void WriteValue(object o)
        {
            switch (o)
            {
                case null:
                    Write("null");
                    break;
                case DateTime time:
                    Write(time.ToShortDateString());
                    break;
                case ValueType _:
                case string _:
                    Write(o.ToString());
                    break;
                case IEnumerable _:
                    Write("...");
                    break;
                default:
                    Write("{ }");
                    break;
            }
        }
    }
}