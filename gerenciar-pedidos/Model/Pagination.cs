

public class Pagination<T>{

    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public int TotalPages { get; set; }

    public List<T> Data { get; set; } = new List<T>();
}