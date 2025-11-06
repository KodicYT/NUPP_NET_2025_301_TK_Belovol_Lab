namespace Library.Common
{
    public static class ItemExtensions
    {
        // Метод розширення для Item
        public static bool IsOld(this Item item)
        {
            return item.Year < 2000;
        }
    }
}
