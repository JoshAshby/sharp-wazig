using System;
using Wasmtime;

namespace sharp_wazig
{
  class Program
  {
    static void Main(string[] args)
    {
      using var engine = new EngineBuilder()
                .WithReferenceTypes(true)
                .Build();

      using var module = Module.FromFile(engine, "./zig/zig-cache/lib/zig.wasm");

      using var host = new Host(engine);

      using var hFun = host.DefineFunction(
          "env",
          "hello",
          () => Console.WriteLine("Hello from C#!")
      );

      using var prFun = host.DefineFunction(
          "env",
          "print",
          (Caller caller, int str_ptr) =>
          {
            var str = caller.GetMemory("memory").ReadNullTerminatedString(str_ptr);
            Console.WriteLine(str);
          }
      );

      using dynamic instance = host.Instantiate(module);
      Console.WriteLine($"1 + 2 = {instance.add(1, 2)} according to Zig");
    }
  }
}
