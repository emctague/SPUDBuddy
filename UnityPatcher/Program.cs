using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using RuntimeInjectedCode;

namespace UnityPatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var asm = AssemblyDefinition.ReadAssembly("../Managed/UnityEngine.CoreModule.dll", new ReaderParameters{ReadingMode=ReadingMode.Immediate, ReadWrite = false, InMemory = true});
            var main = asm.MainModule;
            var app = main.GetType("UnityEngine", "Application");

            var cctor = app.Methods.FirstOrDefault(m => m.IsRuntimeSpecialName && m.Name == ".cctor");
            var cbs = main.ImportReference(((Action)RuntimeInjector.CreateBootstrapper).Method);

            if (cctor == null)
            {
                Console.WriteLine("Injecting Static constructor");
                cctor = new MethodDefinition(".cctor",
                    MethodAttributes.RTSpecialName | MethodAttributes.Static | MethodAttributes.SpecialName,
                    main.TypeSystem.Void);
                
                app.Methods.Add(cctor);
                
                var ilp = cctor.Body.GetILProcessor();
                ilp.Emit(OpCodes.Call, cbs);
                ilp.Emit(OpCodes.Ret);
            }
            else
            {
                Console.WriteLine("Adding to Static Constructor");
                var ilp = cctor.Body.GetILProcessor();
                ilp.Replace(cctor.Body.Instructions[0], ilp.Create(OpCodes.Call, cbs));
                ilp.Replace(cctor.Body.Instructions[1], ilp.Create(OpCodes.Ret));
            }
            
            asm.Write("../Managed/UnityEngine.CoreModule.dll");
           
        }
    }
}