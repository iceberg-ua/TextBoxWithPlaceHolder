using System;
using System.Drawing;
using System.Windows.Forms;

private class TextboxWithPlaceholder : TextBox
{
    private bool _isPassword;
    private bool _showPlaceholder = true;
    private Font _pswdFont = new Font("Wingdings", 13);
    private Font _baseFont = new Font("Arial", 14);

    public TextboxWithPlaceholder(Font baseFont, bool isPassword)
    {
        _baseFont = baseFont;

        Font = baseFont;
        ForeColor = SystemColors.ControlLight;
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);

        Placeholder = "Username";
        IsPassword = isPassword;
        _showPlaceholder = true;
        Text = Placeholder;
        //if(isPassword)
        //    PasswordChar = 'l';

        GotFocus += TextboxGotFocus;
        LostFocus += TextboxLostFocus;
    }

    //protected override void OnPaint(PaintEventArgs e)
    //{
    //    Graphics grfx = e.Graphics;
    //    base.OnPaint(e);

    //    grfx.DrawString(Placeholder, _baseFont, SystemBrushes.ControlDark, Bounds);
    //}

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
    }

    public string Placeholder { get; set; }

    public bool IsPassword
    {
        get { return _isPassword; }
        set
        {
            _isPassword = value;
            if (_isPassword)
                Placeholder = "Password";
        }
    }

    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case (int)WinApi.WM_CHAR:
                base.WndProc(ref m);
                _showPlaceholder = Text == string.Empty;
                return;
        }

        base.WndProc(ref m);
    }

    private void TextboxLostFocus(object sender, EventArgs e)
    {
        if (Text == string.Empty)
        {
            if (IsPassword)
            {
                Font = _baseFont;
                PasswordChar = '\0';
            }

            ForeColor = SystemColors.ControlLight;
            Text = Placeholder;
        }
    }

    private void TextboxGotFocus(object sender, EventArgs e)
    {
        if (_showPlaceholder)
        {
            Text = string.Empty;
            ForeColor = Color.Black;

            if (IsPassword)
            {
                Font = _pswdFont;
                PasswordChar = 'l';
            }
        }
    }
}
