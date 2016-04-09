﻿namespace Atata
{
    [ControlDefinition("a", ComponentTypeName = "link", IgnoreNameEndings = "Button,Link")]
    public class LinkControl<TNavigateTo, TOwner> : ClickableControl<TNavigateTo, TOwner>
        where TNavigateTo : PageObject<TNavigateTo>
        where TOwner : PageObject<TOwner>
    {
    }
}
