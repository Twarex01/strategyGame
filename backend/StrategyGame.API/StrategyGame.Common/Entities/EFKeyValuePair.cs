using System.ComponentModel.DataAnnotations;

namespace StrategyGame.Common.Entities
{
    public class EFKeyValuePair<TKey, TValue>
    {
        [Key]
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
