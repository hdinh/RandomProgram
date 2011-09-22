namespace Dinh.RandomProgram
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;

    public sealed class DynamicProgram
    {
        private readonly LambdaExpression expression;

        public DynamicProgram(LambdaExpression expression) {
            this.expression = expression;
        }

        public void Export(string assemblyName) {
            var domain = AppDomain.CurrentDomain;

            // Create dynamic assembly
            var asmName = new AssemblyName("DynamicAssembly");
            var dynAsm = domain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Save);

            // Create a dynamic module and type
            var dynMod = dynAsm.DefineDynamicModule("dynamicModule", assemblyName);
            var typeBuilder = dynMod.DefineType("dynamicType");

            // Create our method builder for this type builder
            var methodBuilder = typeBuilder.DefineMethod(
                "dynamicMethod",
                MethodAttributes.Public | MethodAttributes.Static);

            this.expression.CompileToMethod(methodBuilder);

            typeBuilder.CreateType();

            dynAsm.Save(assemblyName);
        }
    }
}
