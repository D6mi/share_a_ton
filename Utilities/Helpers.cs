using System.Windows.Forms;
using Share_a_Ton.Udp;

namespace Share_a_Ton.Utilities
{
    public class Helpers
    {
        public static ListViewItem CreateListViewItem(ListView listView, ClientInfo client)
        {
            var item = new ListViewItem {Tag = client, Text = client.ToString()};
            return item;
        }

        public static bool DoesListContainClient(ClientInfo client, ListView list)
        {
            foreach (ListViewItem item in list.Items)
            {
                var clientFromList = (ClientInfo) item.Tag;
                if (clientFromList.Equals(client))
                    return true;
            }
            return false;
        }
    }
}