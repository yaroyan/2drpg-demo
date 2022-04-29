using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MacroCommand : Command
{
    private Stack<Command> commands = new Stack<Command>();
    public void Execute()
    {
        foreach (Command command in commands) command.Execute();
    }

    public void Append(Command command)
    {
        if (command != null && command != this) commands.Push(command);
    }

    public void Undo()
    {
        if (commands.Any()) commands.Pop();
    }

    public void Clear()
    {
        commands.Clear();
    }
}
