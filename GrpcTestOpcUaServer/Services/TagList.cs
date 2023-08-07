namespace GrpcTestOpcUaServer.Services;

public class TagList {

    private static readonly TagList tagList = new TagList();

    private readonly Dictionary<string, string> tags = new();

    static TagList() { }

    private TagList() { }

    public static TagList Instance { 
        get { return tagList; }
    }

    public Dictionary<string, string> GetTags {
        get { return tags; }
    }
}
