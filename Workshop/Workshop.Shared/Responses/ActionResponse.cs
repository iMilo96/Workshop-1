using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Shared.Responses;

public class ActionResponse<T>
{
    public List<T> result;

    public bool WasSuccess { get; set; }
    public string? Message { get; set; }
    public T? Result { get; set; }
}