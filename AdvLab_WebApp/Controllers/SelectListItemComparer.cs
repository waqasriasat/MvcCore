using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdvLab_WebApp.Controllers
{
    public class SelectListItemComparer : IEqualityComparer<SelectListItem>
    {
        public bool Equals(SelectListItem x, SelectListItem y)
        {
            // Compare Value and Text properties
            return x.Value == y.Value && x.Text == y.Text;
        }

        public int GetHashCode(SelectListItem obj)
        {
            // Get hash code for the Value and Text properties
            return obj.Value.GetHashCode() ^ obj.Text.GetHashCode();
        }
    }
}