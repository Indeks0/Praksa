namespace Project.Common.Interfaces
{
    public interface IPaging
    {
        int CurrentPage { get; set; }
        int ItemsPerPage { get; set; }
    }
}