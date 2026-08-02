public interface ICollectible
{
    public bool TryCollect(ref ItemType itemCollected, ref int amount);
    public void OnCollect();
}