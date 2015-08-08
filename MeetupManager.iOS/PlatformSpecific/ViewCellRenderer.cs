using Xamarin.Forms;
using MeetupManager.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(StandardViewCellRenderer))]
namespace MeetupManager.iOS
{
	public class StandardViewCellRenderer : ViewCellRenderer
	{

		public override UIKit.UITableViewCell GetCell (Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
		{
			var cell = base.GetCell (item, reusableCell, tv);
            switch (item.StyleId)
            {
                case "none":
                    cell.Accessory = UIKit.UITableViewCellAccessory.None;
                    break;
                case "checkmark":
                    cell.Accessory = UIKit.UITableViewCellAccessory.Checkmark;
                    break;
                case "detail":
                    cell.Accessory = UIKit.UITableViewCellAccessory.DetailButton;
                    break;
                case "detail-disclosure":
                    cell.Accessory = UIKit.UITableViewCellAccessory.DetailDisclosureButton;
                    break;
                case "disclosure":
                default:
                    cell.Accessory = UIKit.UITableViewCellAccessory.DisclosureIndicator;
                    break;
            }
			return cell;
		}

	}
}