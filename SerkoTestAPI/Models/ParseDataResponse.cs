namespace SerkoTestAPI.Models
{
    public class ParseDataResponse
    {
        public string[] ErrorMessages { get; set; }
        public bool ParseSuccessful { get; set; }
        public Node Data { get; set; }
    }
}