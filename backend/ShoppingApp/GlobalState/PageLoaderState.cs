namespace ShoppingApp.Web.GlobalState;

public class PageLoaderState
{
    private bool _isLoading;

    public event Action? OnChange;

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (_isLoading == value) return;
            _isLoading = value;
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
