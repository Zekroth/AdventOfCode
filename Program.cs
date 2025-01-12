using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

const string cookie = "53616c7465645f5fc12e3246b558a254bf575b01114b24d8cfad6e1f437005c5dfea81ee70ea59e56793445c116b17945f23e144ff23399b723b43e4143212b9";




while (true)
{

    Console.Clear();

    Console.WriteLine("Buon Natale!\nQuale problema vuoi risolvere?");

    int counter = 1;
    List<MethodInfo> mList = new List<MethodInfo>();

    mList.Add(null);

    foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
    {
        foreach (var item in ass.DefinedTypes.Where((x) => x.FullName.StartsWith("AdventOfCode")))
        {
            var methods = item.GetMethods().Where((x) => x.Name.StartsWith("Day"));

            foreach (var method in methods)
            {
                Console.WriteLine($"-{counter}) {method.ReflectedType?.FullName} {method.Name}");
                counter++;
                mList.Add(method);
            }
        }
    }

    var io = Console.ReadLine();
    
    Int32.TryParse(io, out int selected);
    
    if (selected > 0 && selected < mList.Count)
    {

        var task = (Task) (mList[selected].Invoke(null, new object[] { cookie }) ?? Task.CompletedTask);
        await task.ConfigureAwait(false);

        Console.WriteLine("Premi invio per risolvere un altro problema. Premi ESC o Q per uscire.");
        while (true) {
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
            {
                break;
            }

            if(key == ConsoleKey.Q || key == ConsoleKey.Escape)
            {
                return;
            }
        }
        
    } else { Console.WriteLine("Non ho capito, puoi ripetere?"); }

}
