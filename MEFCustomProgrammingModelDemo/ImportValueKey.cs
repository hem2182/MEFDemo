using System;
using System.ComponentModel.Composition.Primitives;

namespace MEFCustomProgrammingModelDemo
{
    public class ImportValueKey : Tuple<string, string>
    {
        public ImportDefinition ImportDefinition { get; set; }
        public string TypeName { get; set; }
        public string Contract { get; set; }

        public ImportValueKey(string typeName, string contractName, ImportDefinition definition)
            : base(typeName, contractName)
        {
            this.ImportDefinition = definition;
            this.TypeName = typeName;
            this.Contract = contractName;
        }
    }
}