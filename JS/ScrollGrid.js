// JScript File


       
        var Grid = null;
        var UpperBound = 0;
        var LowerBound = 1;
        var CollapseImage = 'minus.gif';
        var ExpandImage = 'plus.gif';
        var IsExpanded = true;   
        var Rows = null;
        var n = 1;
        var TimeSpan = 25;
        var SelectedRow = null;
        var SelectedRowIndex = null;
        
        window.onload = function()
        {
            Grid = document.getElementById('<%= this.gvUpdatePrice.ClientID %>');
            UpperBound = parseInt('<%= this.gvUpdatePrice.Rows.Count %>');
            Rows = Grid.getElementsByTagName('tr');
            SelectedRowIndex = -1;  
        }
        
        function Toggle(Image)
        {
            ToggleImage(Image);
            ToggleRows();  
        }    
        
        function ToggleImage(Image)
        {
            if(IsExpanded)
            {
                //Image.src = ExpandImage;
                Image.title = 'Expand';
                Grid.rules = 'none';
                n = LowerBound;
                
                IsExpanded = false;
            }
            else
            {
                //Image.src = CollapseImage;
                Image.title = 'Collapse';
                Grid.rules = 'cols';
                n = UpperBound;
                
                IsExpanded = true;
            }
        }
        
        function ToggleRows()
        {
            if (n < LowerBound || n > UpperBound)  return;
                            
            Rows[n].style.display = Rows[n].style.display == '' ? 'none' : '';
            if(IsExpanded) n--; else n++;
            setTimeout("ToggleRows()",TimeSpan);
        }
        
    function SelectRow(CurrentRow, RowIndex)
    {        
        if(SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound) return;
         
        if(SelectedRow != null)
        {
            SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
            SelectedRow.style.color = SelectedRow.originalForeColor;
        }
        
        if(CurrentRow != null)
        {
            CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
            CurrentRow.originalForeColor = CurrentRow.style.color;
            CurrentRow.style.backgroundColor = 'YellowGreen';
            CurrentRow.style.color = 'Black';
        } 
        
        SelectedRow = CurrentRow;
        SelectedRowIndex = RowIndex;
        setTimeout("SelectedRow.focus();",0); 
    }
    
    function SelectSibling(e)
    { 
        var e = e ? e : window.event;
        var KeyCode = e.which ? e.which : e.keyCode;
        
        if(KeyCode == 40)
            SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
        else if(KeyCode == 38)
            SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);
            
        return false;
    }
        
