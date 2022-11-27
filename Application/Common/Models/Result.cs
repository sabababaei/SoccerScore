namespace Application.Common.Models;

public class Result<T>
{
    internal Result(bool succeeded, IEnumerable<string>? errors , T data)
    {
        Succeeded = succeeded;
        Errors = errors;
        Data = data;
    }

    public bool Succeeded { get;   }

    public IEnumerable<string>? Errors { get;   }
    public T Data { get;   }


}

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors )
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
         
    }

    public bool Succeeded { get;   }

    public string[] Errors { get;   }
 

}