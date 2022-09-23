namespace Bank_API.Version
{
    public class Response
    {
        public string DemandId => $"{Guid.NewGuid().ToString()}";
        public string ResponseId { get; set; }
        public string ResponseChat { get; set; }
        public object Data { get; set; }

    }
}
