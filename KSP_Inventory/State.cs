using System;
namespace inventory
{
    public interface State: IConfigNode
    {
        string GetName();
    }
}
