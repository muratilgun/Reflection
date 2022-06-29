﻿using System;
using System.Reflection;

namespace ReflectionSampleConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region First Module

            #endregion

            var personType = typeof(Person);
            // Private Constructor
            var personConstructors = personType
                .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            //var personConstructors = personType.GetConstructors();

            foreach (var personConstructor in personConstructors)
            {
                Console.WriteLine(personConstructor);
            }

            var privatePersonConstructor = personType.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null, new Type[] { typeof(string), typeof(int) },
                null);
            Console.WriteLine(privatePersonConstructor);

            //Constructor çağırma
            var person1 = personConstructors[0].Invoke(null);
            //Parametre ile ctor çağırma
            var person2 = personConstructors[1].Invoke(new object[] { "Murat" });
            //Overload private ctor çağırma 
            var person3 = personConstructors[2].Invoke(new object[] { "Murat", 29 });


            Console.ReadLine();
        }

        public void InspectingMetadata()
        {
            string name = "Murat";
            //var stringType = name.GetType();
            var stringType = typeof(string);
            Console.WriteLine(stringType);

            var currentAssembly = Assembly.GetExecutingAssembly();
            var typesFromCurrentAssembly = currentAssembly.GetTypes();
            foreach (var type in typesFromCurrentAssembly)
            {
                Console.WriteLine(type.Name);
            }

            var oneTypeFromCurrentAssembly = currentAssembly.GetType("ReflectionSampleConsole.Person");
            Console.WriteLine(oneTypeFromCurrentAssembly.Name);

            var externalAssembly = Assembly.Load("System.Text.Json");
            var typesFromExternalAssembly = externalAssembly.GetTypes();
            var oneTypeFromExternalAssembly = externalAssembly.GetType("System.Text.Json.JsonProperty");

            var modulesFromExternalAssembly = externalAssembly.GetModules();
            var oneModuleFromExternalAssembly = externalAssembly.GetModule("System.Text.Json.dll");

            var typesFromModuleFromExternalAssembly = oneModuleFromExternalAssembly.GetTypes();
            var oneTypeFromModuleFromExternalAssembly =
                oneModuleFromExternalAssembly.GetType("System.Text.Json.JsonProperty");

            foreach (var constructor in oneTypeFromCurrentAssembly.GetConstructors())
            {
                Console.WriteLine(constructor);
            }
            Console.WriteLine("|------------------------------------------|");

            //foreach (var method in oneTypeFromCurrentAssembly.GetMethods())
            //{
            //    Console.WriteLine(method);
            //}

            foreach (var method in oneTypeFromCurrentAssembly.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine($"{method}, public: {method.IsPublic}");
            }

            Console.ReadLine();
        }
    }
}
