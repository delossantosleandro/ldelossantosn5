namespace LdelossantosN5.DAL
{

    /// <summary>
    /// Documental storage in another table could be a better option, but for the sake of the implementation, will work
    /// </summary>
    public class CQRSSEmployeeSecurityEntity
    {
        public int EmployeeId { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}