﻿Public Interface IUserGroup
    Inherits IGroup
    Inherits ISavable

    Property IsActive As Boolean

    Sub Save(ByVal settings As ISettings)
End Interface
