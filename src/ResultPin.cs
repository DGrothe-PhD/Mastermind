namespace MastermindVariante
{
    // Evaluation pins, can be empty, black or white.
    public class ResultPin : Parameter, IMMDispose
    {
        private readonly Form1 caller;
        private readonly Panel panel;
        private readonly int positionX, positionY;

        public ResultPin(Form1 caller, short rowindex, short fieldindex)
        {
            this.caller = caller;

            panel = new Panel
            {
                Size = new Size(width / 2, height / 2),
                Name = $"ResultPinR{rowindex}F{fieldindex}",
                BackColor = Color.Transparent,
                BackgroundImage = Properties.Resources.emptypin,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            positionX = initialPositionX
                + caller.WordLength * (width + paddingLeft) + 2 * paddingLeft
                + fieldindex.WrapCol(pinsPerRow) * (int)(width + paddingLeft) / 2;
            positionY = initialPositionY
                + (2 * rowindex + fieldindex.WrapRow(pinsPerRow)) * (int)(height + paddingBottom) / 2;
            panel.Location = new Point(positionX, positionY);
            caller.Controls.Add(panel);
        }


        public void MakeWhite()
        {
            panel.BackgroundImage = Properties.Resources.whitepin;
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            panel.Refresh();
        }

        public void MakeBlack()
        {
            panel.BackgroundImage = Properties.Resources.blackpin;
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            panel.Refresh();
        }

        public void MakeEmpty()
        {
            panel.BackgroundImage = Properties.Resources.emptypin;
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            panel.Refresh();
        }

        public void MoveBy(int x, int y)
        {
            panel.Location = new Point(panel.Location.X + x, panel.Location.Y + y);
        }

        public void Remove()
        {
            caller.Controls.Remove(panel);
            panel.Dispose();
        }

    }

}
