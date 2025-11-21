

public class ResponseModel
{
    public string message { get; set; }
    public bool Succes { get; set; }

    public ResponseModel(string message = "", bool Succes = false)
    {
        this.message = message;
        this.Succes = Succes;
    }
}