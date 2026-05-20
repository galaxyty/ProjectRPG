public interface ISaveService
{
    /// <summary>
    /// 저장.
    /// </summary>
    public void Save(string json);

    /// <summary>
    /// 로드.
    /// </summary>
    public string Load();
}
