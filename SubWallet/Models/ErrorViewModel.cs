namespace WebApplication1.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; } //research this 

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}