using UnityEngine;

public abstract class BasePresenter<TView, TModel>
    where TView : BaseView
    where TModel : BaseModel
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
    /// ºä ¼ÂÆÃ.
    /// </summary>
    public void SetView(TView view)
    {
        _view = view;        
    }

    /// <summary>
    /// ¸ðµ¨ ¼ÂÆÃ.
    /// </summary>    
    public void SetModel(TModel model)
    {
        _model = model;

        OnBindModel();
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
