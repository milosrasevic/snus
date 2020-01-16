using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScadaModels;

    namespace DatabaseManager
{
    public partial class Form1 : Form
    {
        public functionType fType;
        int address;
        string id;

        Driver selDriver;

        ServiceReference1.DatabaseManagerClient proxy;
        public Form1(ServiceReference1.DatabaseManagerClient _proxy)
        {
            proxy = _proxy;
            InitializeComponent();
            function.SelectedIndex = 0;
            tagType.SelectedIndex = 0;
            driver.SelectedIndex = 0;
            driverSubmit.SelectedIndex = 0;
            scan.Enabled = true;

            var tags = proxy.getTags();
            var units = proxy.getUnits();
            Tag t;
            SimulationUnit u;
            
            
            foreach (var tag in tags.Keys)
            {
                t = tags[tag];
                var item = new ListViewItem(t.tagId);
                item.SubItems.Add(t.description);
                item.SubItems.Add(t.driver.ToString());
                item.SubItems.Add(t.ioAddress.ToString());
                if (t is AITag)
                {
                    var ti = (AITag)t;
                    item.SubItems.Add(ti.scanTime.ToString());
                    item.SubItems.Add(ti.alarm.floor.ToString());
                    item.SubItems.Add(ti.alarm.ceiling.ToString());
                    item.SubItems.Add(ti.on ? "Yes":"No");
                    item.SubItems.Add(ti.auto ? "Yes":"No");
                    item.SubItems.Add("");
                    item.SubItems.Add(ti.low.ToString());
                    item.SubItems.Add(ti.high.ToString());
                    item.SubItems.Add(ti.units);
                }
                else if (t is AOTag)
                {
                    var ti = (AOTag)t;
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add(ti.initialValue.ToString());
                    item.SubItems.Add(ti.low.ToString());
                    item.SubItems.Add(ti.high.ToString());
                    item.SubItems.Add(ti.units);
                }
                else if (t is DITag)
                {
                    var ti = (DITag)t;
                    item.SubItems.Add(ti.scanTime.ToString());
                    item.SubItems.Add(ti.alarm.floor.ToString());
                    item.SubItems.Add(ti.alarm.ceiling.ToString());
                    item.SubItems.Add(ti.on ? "Yes" : "No");
                    item.SubItems.Add(ti.auto ? "Yes" : "No");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                }
                else if (t is DOTag)
                {
                    var ti = (DOTag)t;
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add(ti.initialValue.ToString());
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                }
                listViewTag.Items.Add(item);
                
            }
            
            foreach(var unit in units.Keys)
            {
                u = units[unit];
                ListViewItem item = new ListViewItem(u.id);
                item.SubItems.Add(u.address.ToString());
                item.SubItems.Add(u.fType.ToString());

                listViewUnit.Items.Add(item);
                
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = function.SelectedIndex;
            fType = (functionType) selectedIndex;

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            id = unitID.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitID.Text) || unitAddress.Value < 0 || unitAddress.Value > 4)
                return;
            if (proxy.addNewSimulationDriverUnit(id, fType, address))
            {
                ListViewItem item = new ListViewItem(id);
                item.SubItems.Add(address.ToString());
                item.SubItems.Add(fType.ToString());

                listViewUnit.Items.Add(item);

                unitID.Clear();
                function.SelectedIndex = 0;
                unitAddress.Value = 0;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            address = (int) unitAddress.Value;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTag.SelectedItems.Count == 0)
                return;
            var firstSelectedItem = listViewTag.SelectedItems[0];
            tagNameSubmit.Text = firstSelectedItem.Text;

        }

        private void removeUnitBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idUnitRemove.Text))
                return;
            if (proxy.removeSimulationDriverUnit(idUnitRemove.Text))
            {
                foreach(ListViewItem item in listViewUnit.Items)
                {
                    if(item.Text == idUnitRemove.Text)
                        listViewUnit.Items.Remove(item);
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            Tag t = null;
            
            switch (tagType.SelectedItem)
            {
                case "Digital input":
                    if (string.IsNullOrEmpty(tagName.Text) || string.IsNullOrEmpty(description.Text) || scanTime.Value < 1 || alarmCeiling.Value < alarmFloor.Value)
                        return;
                    t = new DITag(tagName.Text, description.Text, (int)ioAddress.Value, selDriver, (int)scanTime.Value, new Alarm((int)alarmCeiling.Value, (int)alarmFloor.Value), scan.Checked, auto.Checked);
                    break;
                case "Digital output":
                    if (string.IsNullOrEmpty(tagName.Text) || string.IsNullOrEmpty(description.Text))
                        return;
                    t = new DOTag(tagName.Text, description.Text, (int)ioAddress.Value, selDriver, (int)initialValue.Value);
                    break;
                case "Analog input":
                    if (string.IsNullOrEmpty(tagName.Text) || string.IsNullOrEmpty(description.Text) || scanTime.Value < 1 || alarmCeiling.Value < alarmFloor.Value || limitLow.Value > limitHigh.Value || string.IsNullOrEmpty(units.Text))
                    return;
                        t = new AITag(tagName.Text, description.Text, (int)ioAddress.Value, selDriver, (int)scanTime.Value, new Alarm((int)alarmCeiling.Value, (int)alarmFloor.Value), scan.Checked, auto.Checked, (int)limitLow.Value, (int)limitHigh.Value, units.Text);
                    break;
                case "Analog output":
                    if (string.IsNullOrEmpty(tagName.Text) || string.IsNullOrEmpty(description.Text) || limitLow.Value > limitHigh.Value || string.IsNullOrEmpty(units.Text))
                        return;
                    t = new AOTag(tagName.Text, description.Text, (int)ioAddress.Value, selDriver, (int)initialValue.Value, (int)limitLow.Value, (int)limitHigh.Value, units.Text);
                    break;
                default:
                    break;
            }

            if (proxy.addTag(t))
            {
                var item = listViewTag.Items.Add(new ListViewItem(tagName.Text));
                item.SubItems.Add(description.Text);
                item.SubItems.Add(selDriver.ToString());
                item.SubItems.Add(ioAddress.Value.ToString());
                item.SubItems.Add(scanTime.Value.ToString());
                item.SubItems.Add(alarmFloor.Value.ToString());
                item.SubItems.Add(alarmCeiling.Value.ToString());
                item.SubItems.Add(scan.Checked ? "Yes" : "No");
                item.SubItems.Add(auto.Checked ? "Yes" : "No");
                item.SubItems.Add(initialValue.Value.ToString());
                item.SubItems.Add(limitLow.Value.ToString());
                item.SubItems.Add(limitHigh.Value.ToString());
                item.SubItems.Add(units.Text);
            }


        }

        private void driver_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(driver.SelectedItem.ToString(), out selDriver);

            switch (selDriver)
            {
                case Driver.SimulationDriver:
                    auto.Enabled = true;
                    break;
                case Driver.RealTimeDriver:
                    auto.Checked = true;
                    auto.Enabled = false;
                    break;
                case Driver.OutputDriver:
                    auto.Checked = false;
                    auto.Enabled = false;
                    scan.Enabled = false;
                    scan.Checked = false;
                    break;
                default:
                    break;
            }
        }

        private void tagType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tagType.SelectedItem)
            {
                case "Digital input":
                    scanTime.Enabled = true;
                    alarmCeiling.Enabled = true;
                    alarmFloor.Enabled = true;
                    scan.Enabled = true;
                    auto.Enabled = true;
                    initialValue.Enabled = false;
                    driver.Items.Clear();
                    driver.Items.Add("SimulationDriver");
                    driver.Items.Add("RealTimeDriver");
                    driver.SelectedIndex = 0;
                    limitLow.Enabled = false;
                    limitHigh.Enabled = false;
                    units.Enabled = false;
                    break;
                case "Digital output":
                    scanTime.Enabled = false;
                    alarmCeiling.Enabled = false;
                    alarmFloor.Enabled = false;
                    scan.Enabled = false;
                    auto.Enabled = false;
                    initialValue.Enabled = true;
                    driver.Items.Clear();
                    driver.Items.Add("OutputDriver");
                    driver.SelectedIndex = 0;
                    limitLow.Enabled = false;
                    limitHigh.Enabled = false;
                    units.Enabled = false;
                    break;
                case "Analog input":
                    scanTime.Enabled = true;
                    alarmCeiling.Enabled = true;
                    alarmFloor.Enabled = true;
                    scan.Enabled = true;
                    auto.Enabled = true;
                    initialValue.Enabled = false;
                    driver.Items.Clear();
                    driver.Items.Add("SimulationDriver");
                    driver.Items.Add("RealTimeDriver");
                    driver.SelectedIndex = 0;
                    limitLow.Enabled = true;
                    limitHigh.Enabled = true;
                    units.Enabled = true;
                    break;
                case "Analog output":
                    scanTime.Enabled = false;
                    alarmCeiling.Enabled = false;
                    alarmFloor.Enabled = false;
                    scan.Enabled = false;
                    auto.Enabled = false;
                    initialValue.Enabled = true;
                    driver.Items.Clear();
                    driver.Items.Add("OutputDriver");
                    driver.SelectedIndex = 0;
                    limitLow.Enabled = true;
                    limitHigh.Enabled = true;
                    units.Enabled = true;
                    break;
                default:
                    break;
                
            }
            if(selDriver == Driver.RealTimeDriver)
            {
                auto.Checked = true;
                auto.Enabled = false;
            }else if (selDriver == Driver.OutputDriver) { 
                auto.Checked = false;
                auto.Enabled = false;
                scan.Enabled = false;
                scan.Checked = false;

            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tagNameSubmit.Text))
                return;
            if (proxy.removeTag(tagNameSubmit.Text))
            {
                foreach (ListViewItem item in listViewTag.Items)
                {
                    if (item.Text == tagNameSubmit.Text)
                        listViewTag.Items.Remove(item);
                }
            }
        }

        private void autoBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tagNameSubmit.Text))
                return;

            Dictionary<TagAttribute, object> changes = new Dictionary<TagAttribute, object>();
            changes[TagAttribute.auto] = true;
            if (proxy.changeTag(tagNameSubmit.Text, changes))
            {
                foreach (ListViewItem item in listViewTag.Items)
                {
                    if (item.Text == tagNameSubmit.Text)
                        item.SubItems[8].Text = "Yes";
                }
            }
        }

        private void scanBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tagNameSubmit.Text))
                return;

            Dictionary<TagAttribute, object> changes = new Dictionary<TagAttribute, object>();
            changes[TagAttribute.on] = true;
            if (proxy.changeTag(tagNameSubmit.Text, changes))
            {
                foreach (ListViewItem item in listViewTag.Items)
                {
                    if (item.Text == tagNameSubmit.Text)
                        item.SubItems[7].Text = "Yes";
                }
            }
        }

        private void scanOffBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tagNameSubmit.Text))
                return;

            Dictionary<TagAttribute, object> changes = new Dictionary<TagAttribute, object>();
            changes[TagAttribute.on] = false;
            if (proxy.changeTag(tagNameSubmit.Text, changes))
            {
                foreach (ListViewItem item in listViewTag.Items)
                {
                    if (item.Text == tagNameSubmit.Text)
                        item.SubItems[7].Text = "No";
                }
            }
        }

        private void manualBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tagNameSubmit.Text))
                return;

            Dictionary<TagAttribute, object> changes = new Dictionary<TagAttribute, object>();
            changes[TagAttribute.auto] = false;
            if (proxy.changeTag(tagNameSubmit.Text, changes))
            {
                foreach (ListViewItem item in listViewTag.Items)
                {
                    if (item.Text == tagNameSubmit.Text)
                        item.SubItems[8].Text = "No";
                }
            }
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            Dictionary<TagAttribute, object> changes = new Dictionary<TagAttribute, object>();

            if (descriptionAllow.Checked)
            {
                if(!string.IsNullOrEmpty(descriptionSubmit.Text))
                {
                    changes[TagAttribute.description] = descriptionSubmit.Text;
                }
            }
            if (driverAllow.Checked)
            {
                changes.Add(TagAttribute.driver, (string)driverSubmit.SelectedItem == "Real time driver" ? "RealTimeDriver" : "SimulationDriver");
            }
            if (alarmAllow.Checked)
            {
                if(alarmFloorSubmit.Value < alarmCeilingSubmit.Value)
                    changes[TagAttribute.alarm] = alarmCeilingSubmit.Value.ToString() + "|" + alarmFloorSubmit.Value.ToString();

            }
            if (initialValueAllow.Checked)
            {
                changes[TagAttribute.initialValue] = (int)initialValueSubmit.Value;
            }
            if (limitAllow.Checked)
            {
                if (limitLowSubmit.Value < limitHighSubmit.Value)
                {
                    changes[TagAttribute.low] = (int)limitLowSubmit.Value;
                    changes[TagAttribute.high] = (int)limitHighSubmit.Value;
                }
            }
            if (unitsAllow.Checked)
            {
                if (string.IsNullOrEmpty(unitsSubmit.Text))
                    changes[TagAttribute.units] = unitsSubmit.Text;
            }

            if(proxy.changeTag(tagNameSubmit.Text, changes)){
                ListViewItem item = null;
                foreach(ListViewItem i in listViewTag.Items)
                {
                    if (i.Text == tagNameSubmit.Text)
                        item = i;
                }
                foreach(var change in changes.Keys)
                    switch (change)
                    {
                        case TagAttribute.tagId:
                            break;
                        case TagAttribute.description:
                            item.SubItems[1].Text = (string) changes[change];
                            break;
                        case TagAttribute.driver:
                            item.SubItems[2].Text = changes[change].ToString();
                            break;
                        case TagAttribute.ioAddress:
                            item.SubItems[3].Text = changes[change].ToString();
                            break;
                        case TagAttribute.scanTime:
                            item.SubItems[4].Text = changes[change].ToString();
                            break;
                        case TagAttribute.alarm:
                            string[] alarmValues = ((string)changes[change]).Split('|');
                            item.SubItems[6].Text = alarmValues[0];
                            item.SubItems[5].Text = alarmValues[1];
                            break;
                        case TagAttribute.on:
                            break;
                        case TagAttribute.auto:
                            break;
                        case TagAttribute.initialValue:
                            item.SubItems[9].Text = changes[change].ToString();
                            break;
                        case TagAttribute.low:
                            item.SubItems[10].Text = changes[change].ToString();
                            break;
                        case TagAttribute.high:
                            item.SubItems[11].Text = changes[change].ToString();
                            break;
                        case TagAttribute.units:
                            item.SubItems[12].Text = (string)changes[change];
                            break;
                        default:
                            break;
                    }
            }
        }

        private void listViewUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewUnit.SelectedItems.Count == 0)
                return;
            var firstSelectedItem = listViewUnit.SelectedItems[0];
            idUnitRemove.Text = firstSelectedItem.Text;
        }

        private void alarmCeiling_ValueChanged(object sender, EventArgs e)
        {

        }

        private void scanTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tagNameSubmit_TextChanged(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listViewTag.Items)
            {
                if(item.Text == tagNameSubmit.Text)
                {
                    if(item.SubItems[2].Text == "OutputDriver")
                    {
                        driverSubmit.Items.Clear();
                        driverSubmit.Items.Add("OutputDriver");
                        driverSubmit.SelectedIndex = 0;
                    }
                    else
                    {

                        driverSubmit.Items.Clear();
                        driverSubmit.Items.Add("SimulationDriver");
                        driverSubmit.Items.Add("RealTimeDriver");
                        driverSubmit.SelectedIndex = 0;
                    }
                }
            }
        }
    }
}
