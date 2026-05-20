public abstract class BasePresenter<TView, TModel>
{
    /// <summary>
    /// ºä.
    /// </summary>
    protected TView _view;

    /// <summary>
    /// ¸ðµ¨.
    /// </summary>
    protected TModel _model;

    /// <summary>
    /// ¸ðµ¨, ºä ¼ÂÆÃ.
    /// </summary>
    public void SetModelView(TModel model, TView view)
    {
        _model = model;
        _view = view;        
    }

    /// <summary>
    /// ÇÁ·¹Á¨ÅÍ ÃÊ±âÈ­.
    /// </summary>
    public abstract void Initialization();

    /// <summary>
    /// ¸ðµ¨ÀÌ ±³Ã¼µÉ ¶§¸¶´Ù È£Ãâ.
    /// </summary>
    protected abstract void OnBindModel();
}
