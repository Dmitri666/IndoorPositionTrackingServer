namespace Lps.Contracts.Helper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public static class Help
    {       
        /// <summary>
        /// Liefert den Wert des DescriptionAttributes, sofern vorhanden.
        /// </summary>
        /// <param name="value">Der Enum-Wert.</param>
        /// <returns>Der Wert des DescriptionAttributes oder NULL.</returns>
        public static string GetDescription(this Enum value)
        {
            return GetDescription<DescriptionAttribute>(value, a => a.Description);
        }

        private static string GetDescription<T>(Enum value, Func<T, string> getDescription)
           where T : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            var field = type.GetField(name);
            if (field == null)
            {
                return null;
            }

            var attr = Attribute.GetCustomAttribute(field, typeof(T)) as T;
            if (attr != null && getDescription != null)
            {
                return getDescription(attr);
            }

            return null;
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
                        yield return new T[] { item }.Concat(result);
                }

                ++i;
            }

            //char[] inputSet = { 'A', 'B', 'C', 'D' };

            //Combinations<char> combinations = new Combinations<char>(inputSet, 3);
            //string cformat = "Combinations of {{A B C D}} choose 3: size = {0}";
            //Console.WriteLine(String.Format(cformat, combinations.Count));
            //foreach (IList<char> c in combinations)
            //{
            //    Console.WriteLine(String.Format("{{{0} {1} {2}}}", c[0], c[1], c[2]));
            //}
        }        
    }
}
