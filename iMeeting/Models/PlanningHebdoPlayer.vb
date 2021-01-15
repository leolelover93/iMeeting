Public Class PlanningHebdoPlayer
    Public Property date_debut As Date
    Public Property date_fin As Date

    Public Property Views As New List(Of ViewPlayer)

    'Const default_max_elts_par_page As Integer = 4
    'Private default_page_show_time As Integer = CInt(ConfigurationManager.AppSettings("PlayerPageShowTime")) '5000 ' ou 30000
    'Const default_on_hide_time As Integer = 1000
    'Const default_on_show_time As Integer = 2000
    'Const default_max_chars_page As Integer = 400
    'Const default_max_chars_page_jour As Integer = 300
    'Const default_div_class_name As String = "StylePageDefault"
    'Const id_salle_conseil As Integer = 2

    ' Par défaut le lundi et le vendredi de la date système
    Public Sub New(player_id As Long)
        Me.date_debut = Now.Date.AddDays(1 - Now.DayOfWeek)
        Me.date_fin = Me.date_debut.AddDays(4)

        _player_id = player_id

        setPlayerPages()
    End Sub

    Private Property _player_id As Long

    '' Par défaut le lundi et le vendredi de la date en paramètres
    'Public Sub New(une_date As Date)
    '    Me.date_debut = une_date.Date.AddDays(1 - une_date.DayOfWeek)
    '    Me.date_fin = Me.date_debut.AddDays(4)

    '    setPlayerPages()
    'End Sub

    'Public Sub New(date_debut As Date, date_fin As Date)
    '    Me.date_debut = date_debut
    '    Me.date_fin = date_fin

    '    setPlayerPages()
    'End Sub

    Public Function RandomNumber(ByVal MaxNumber As Integer, Optional ByVal MinNumber As Integer = 0) As Integer

        'initialize random number generator
        Dim r As New Random(System.DateTime.Now.Millisecond)

        'if passed incorrect arguments, swap them
        'can also throw exception or return 0

        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return r.Next(MinNumber, MaxNumber)
    End Function

    Private Sub RandomizeArray(ByVal items() As String)
        Dim max_index As Integer = items.Length - 1
        Dim rnd As New Random
        For i As Integer = 0 To max_index - 1
            ' Pick an item for position i.
            Dim j As Integer = rnd.Next(i, max_index + 1)

            ' Swap them.
            Dim temp As String = items(i)
            items(i) = items(j)
            items(j) = temp
        Next i
    End Sub


    '' Remplit le nombre de pages
    'Private Sub setPlayerPages()
    '    Dim db = New ApplicationDbContext

    '    '///////////////////////////////////////////////////////////////////////////////////////////////////////////
    '    '/// Vue hebdomadaire
    '    '///////////////////////////////////////////////////////////////////////////////////////////////////////////
    '    Dim datelundi = Now.Date ' date_debut
    '    Dim rnd As New System.Random
    '    Dim datevendredi = date_fin.AddDays(1) ' On ajuste pour la requête
    '    Dim transitions = {"flipHorz", "flipVert", "scrollHorz"} ', "tileSlide", "fade", "scrollVert","fadeout", "shuffle", , "tileBlind"
    '    Dim transitionsCount = transitions.Count
    '    Dim themes = {"StylePageDefault", "theme1", "theme2", "theme3", "theme4", "theme5", "theme6"}
    '    Dim themesCount = themes.Count

    '    RandomizeArray(transitions)
    '    RandomizeArray(themes)

    '    Dim entities = (From e In db.RESERVATION.Include("LIEUX")
    '                   Where e.ETAT = EnumReservation.Confirmed And e.DATE_DEBUT >= datelundi And e.DATE_DEBUT <= datevendredi
    '                   Order By e.ID_LIEU, e.HEURE_DEBUT Ascending
    '                   Select e).ToList

    '    ' On récupère les lieux concernés
    '    Dim lieux = (From e In entities
    '                Select Id = e.ID_LIEU, Libelle = e.LIEUX.LIBELLE).Distinct

    '    Dim iPage = 1 ' compte le nombre de page
    '    Dim iAnim = 0 ' RandomNumber(transitionsCount) ' index animation
    '    Dim iTheme = 0 ' RandomNumber(themesCount) ' index Thème
    '    Dim PageIndex = 1 ' Numéro de la page au sein de la view
    '    Dim iCount = 0 ' compte le nombre d'elts par lieu ou salle
    '    Dim iLine = 0 ' compte le nombre d'elts par page
    '    Dim iChars = 0 ' compte le nombre de caractères par page
    '    Dim time_by_elt = default_page_show_time \ 4 ' environ 4 éléments par page
    '    For Each lieu In lieux
    '        PageIndex = 1
    '        iCount = 0
    '        Dim myView As New ViewPlayer
    '        myView.Id = iPage
    '        myView.IdLieu = lieu.Id
    '        myView.LibelleLieu = lieu.Libelle
    '        myView.ViewType = EnumViewType.Hebdomadaire
    '        myView.LibellePeriode = String.Format("Semaine du {0} au {1}", date_debut.ToShortDateString, date_fin.ToShortDateString)
    '        myView.AnimationTime = default_on_show_time
    '        myView.MaxPageElements = default_max_elts_par_page

    '        ' Cas de la salle du conseil
    '        If lieu.Id = id_salle_conseil Then
    '            myView.DivClassName = "StylePageConseil"
    '            myView.MaxPageElements = 6
    '        Else
    '            myView.DivClassName = themes(iTheme Mod themesCount) : iTheme += 1 ' default_div_class_name
    '        End If

    '        myView.Animation = transitions(iAnim Mod transitionsCount) : iAnim += 1

    '        Dim elts_page = entities.Where(Function(e) e.ID_LIEU = lieu.Id)

    '        iLine = 0 ' compte le nombre d'elts par page
    '        iCount = 0 ' compte le nombre d'elts par lieu ou salle
    '        iChars = 0
    '        Dim myPage As New PagePlayer
    '        myPage.OnShowTime = default_on_show_time
    '        myPage.OnHideTime = default_on_hide_time

    '        For Each elt In elts_page
    '            If iChars > 0 AndAlso (iChars + elt.Objet.Length) >= default_max_chars_page Then
    '                myView.Pages.Add(myPage)
    '                iLine = 0
    '                iChars = 0

    '                iPage += 1
    '                PageIndex += 1
    '                myPage = New PagePlayer
    '                myPage.OnShowTime = default_on_show_time
    '                myPage.OnHideTime = default_on_hide_time

    '                myPage.Id = iPage
    '            End If

    '            ' Ajouter la réservation à la page
    '            myPage.Reservations.Add(elt)

    '            iChars += elt.Objet.Length
    '            iLine += 1
    '            iCount += 1
    '            If iLine = myView.MaxPageElements Then
    '                myView.Pages.Add(myPage)
    '                iLine = 0
    '                iChars = 0
    '                ' Créer une nouvelle page s'il y a encore des éléments
    '                If iCount < elts_page.Count Then
    '                    iPage += 1
    '                    PageIndex += 1
    '                    myPage = New PagePlayer
    '                    myPage.OnShowTime = default_on_show_time
    '                    myPage.OnHideTime = default_on_hide_time

    '                    myPage.Id = iPage
    '                End If
    '            End If
    '        Next
    '        If iLine <> 0 Then
    '            myView.Pages.Add(myPage)
    '        End If
    '        ' Calculer le temps des pages en fonction du nombre d'éléments affichés
    '        For Each pg In myView.Pages
    '            pg.PageShowTime = pg.Reservations.Count * time_by_elt
    '        Next
    '        ' Calculer le temps de la vue
    '        myView.ViewShowTime = myView.Pages.Sum(Function(e) e.PageShowTime)
    '        ' Ajouter la vue
    '        Views.Add(myView)
    '        iPage += 1
    '    Next
    '    '///////////////////////////////////////////////////////////////////////////////////////////////////////////
    '    '/// Vue journaliere
    '    '///////////////////////////////////////////////////////////////////////////////////////////////////////////
    '    Dim date_debut_jour = Now.Date
    '    Dim date_fin_jour = date_debut_jour.AddHours(24)
    '    Dim elts_page_day = (From e In db.RESERVATION.Include("LIEUX")
    '                   Where e.ETAT = EnumReservation.Confirmed And e.DATE_DEBUT >= date_debut_jour And e.DATE_DEBUT <= date_fin_jour
    '                   Order By e.HEURE_DEBUT Ascending
    '                   Select e).ToList

    '    If elts_page_day.Count > 0 Then
    '        PageIndex = 1
    '        iCount = 0
    '        Dim myViewDay As New ViewPlayer
    '        myViewDay.Id = iPage
    '        myViewDay.IdLieu = 0
    '        myViewDay.LibelleVue = ConfigurationManager.AppSettings("PlayerDayTitle") '"Planning journalier des réunions"
    '        myViewDay.ViewType = EnumViewType.Journalier
    '        myViewDay.LibellePeriode = String.Format("Réunions du {0}", date_debut_jour.ToString("dddd dd/MM/yyyy"))
    '        myViewDay.AnimationTime = default_on_show_time
    '        myViewDay.DivClassName = themes(iTheme Mod themesCount) : iTheme += 1
    '        myViewDay.MaxPageElements = default_max_elts_par_page + 1

    '        myViewDay.Animation = transitions(iAnim Mod transitionsCount) : iAnim += 1

    '        iLine = 0 ' compte le nombre d'elts par page
    '        iCount = 0 ' compte le nombre d'elts par lieu ou salle
    '        iChars = 0
    '        Dim myPageDay As New PagePlayer
    '        myPageDay.OnShowTime = default_on_show_time
    '        myPageDay.OnHideTime = default_on_hide_time
    '        myPageDay.PageShowTime = default_page_show_time

    '        For Each elt In elts_page_day
    '            If iChars > 0 AndAlso (iChars + elt.Objet.Length) >= default_max_chars_page_jour Then
    '                myViewDay.Pages.Add(myPageDay)
    '                iLine = 0
    '                iChars = 0

    '                iPage += 1
    '                PageIndex += 1
    '                myPageDay = New PagePlayer
    '                myPageDay.OnShowTime = default_on_show_time
    '                myPageDay.OnHideTime = default_on_hide_time

    '                myPageDay.Id = iPage
    '            End If

    '            ' Ajouter la réservation à la page
    '            myPageDay.Reservations.Add(elt)

    '            iChars += elt.Objet.Length

    '            iLine += 1
    '            iCount += 1
    '            If iLine = myViewDay.MaxPageElements Then
    '                myViewDay.Pages.Add(myPageDay)
    '                iLine = 0
    '                iChars = 0

    '                ' Créer une nouvelle page s'il y a encore des éléments
    '                If iCount < elts_page_day.Count Then
    '                    iPage += 1
    '                    PageIndex += 1
    '                    myPageDay = New PagePlayer
    '                    myPageDay.OnShowTime = default_on_show_time
    '                    myPageDay.OnHideTime = default_on_hide_time
    '                    myPageDay.PageShowTime = default_page_show_time

    '                    myPageDay.Id = iPage
    '                End If
    '            End If
    '        Next
    '        If iLine <> 0 Then
    '            myViewDay.Pages.Add(myPageDay)
    '        End If

    '        ' Calculer le temps des pages en fonction du nombre d'éléments affichés
    '        For Each pg In myViewDay.Pages
    '            pg.PageShowTime = pg.Reservations.Count * time_by_elt
    '        Next
    '        ' Calculer le temps de la vue
    '        myViewDay.ViewShowTime = myViewDay.Pages.Sum(Function(e) e.PageShowTime)
    '        ' Ajouter la vue
    '        Views.Add(myViewDay)
    '    End If
    'End Sub

    ' Remplit le nombre de pages
    Private Sub setPlayerPages()
        Dim db = New ApplicationDbContext

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/// Vue hebdomadaire
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////
        ' Récupérer la liste des lieux exclus du planning journalier
        Dim black_list_hebdo = (From p In db.PlanningSettings
                        Where p.LieuId > 0 And
                              (p.PointAffichageId = _player_id Or p.PointAffichageId = 0) And
                               p.Afficher = False And
                               p.ViewType = EnumViewType.Hebdomadaire
                        Select p.LieuId).ToList

        Dim datelundi = Now.Date ' date_debut
        Dim rnd As New System.Random
        Dim datevendredi = date_fin.AddDays(1) ' On ajuste pour la requête
        Dim transitions = {"flipHorz", "flipVert", "scrollHorz"} ', "tileSlide", "fade", "scrollVert","fadeout", "shuffle", , "tileBlind"
        Dim transitionsCount = transitions.Count
        Dim themes = {"StylePageDefault", "theme1", "theme2", "theme3", "theme4", "theme5", "theme6"}
        Dim themesCount = themes.Count

        RandomizeArray(transitions)
        RandomizeArray(themes)

        Dim entities = (From e In db.RESERVATION.Include("LIEUX")
                       Where e.ETAT = EnumReservation.Confirmed And e.DATE_DEBUT >= datelundi And e.DATE_DEBUT <= datevendredi And
                             Not black_list_hebdo.Contains(e.ID_LIEU)
                       Order By e.ID_LIEU, e.HEURE_DEBUT Ascending
                       Select e).ToList

        ' On récupère les lieux concernés
        Dim lieux = (From e In entities
                    Select Id = e.ID_LIEU, Libelle = e.LIEUX.LIBELLE).Distinct

        Dim iPage = 1 ' compte le nombre de page
        Dim iAnim = 0 ' RandomNumber(transitionsCount) ' index animation
        Dim iTheme = 0 ' RandomNumber(themesCount) ' index Thème
        Dim PageIndex = 1 ' Numéro de la page au sein de la view
        Dim iCount = 0 ' compte le nombre d'elts par lieu ou salle
        Dim iLine = 0 ' compte le nombre d'elts par page
        Dim iChars = 0 ' compte le nombre de caractères par page
        Dim time_by_elt = 0, time_by_chars = 0 ' environ 4 éléments par page
        For Each lieu In lieux
            Dim param = (From p In db.PlanningSettings
                        Where p.LieuId = lieu.Id And
                               (p.PointAffichageId = _player_id Or p.PointAffichageId = 0) And
                               p.ViewType = EnumViewType.Hebdomadaire
                        Select p).FirstOrDefault

            If IsNothing(param) Then
                param = (From p In db.PlanningSettings
                        Where p.LieuId = 0 And p.PointAffichageId = 0 And
                               p.ViewType = EnumViewType.Hebdomadaire
                        Select p).FirstOrDefault
            End If

            If IsNothing(param) Then
                Continue For
            End If

            time_by_elt = param.ViewShowTime \ param.MaxPageElements
            time_by_chars = param.ViewShowTime \ param.MaxCharsPage
            PageIndex = 1
            iCount = 0
            Dim myView As New ViewPlayer
            myView.Id = iPage
            myView.IdLieu = lieu.Id
            myView.LibelleLieu = lieu.Libelle
            myView.LibelleVue = param.LibelleVue
            myView.ViewType = EnumViewType.Hebdomadaire
            myView.LibellePeriode = String.Format(param.LibellePeriode, date_debut.ToShortDateString, date_fin.ToShortDateString)
            myView.AnimationTime = param.AnimationTime
            myView.MaxPageElements = param.MaxPageElements

            If Not String.IsNullOrWhiteSpace(param.DivClassName) Then
                myView.DivClassName = param.DivClassName '"StylePageConseil" pour la salle du conseil par exemple
            Else
                myView.DivClassName = themes(iTheme Mod themesCount) : iTheme += 1 ' default_div_class_name
            End If

            If Not String.IsNullOrWhiteSpace(param.Animation) Then
                myView.Animation = param.Animation
            Else
                myView.Animation = transitions(iAnim Mod transitionsCount) : iAnim += 1
            End If

            Dim elts_page = entities.Where(Function(e) e.ID_LIEU = lieu.Id)

            iLine = 0 ' compte le nombre d'elts par page
            iCount = 0 ' compte le nombre d'elts par lieu ou salle
            iChars = 0
            Dim myPage As New PagePlayer

            For Each elt In elts_page
                If iChars > 0 AndAlso (iChars + elt.Objet.Length) >= param.MaxCharsPage Then
                    myPage.PageChars = iChars
                    myView.Pages.Add(myPage)
                    iLine = 0
                    iChars = 0

                    iPage += 1
                    PageIndex += 1
                    myPage = New PagePlayer

                    myPage.Id = iPage
                End If

                ' Ajouter la réservation à la page
                myPage.Reservations.Add(elt)

                iChars += elt.Objet.Length
                iLine += 1
                iCount += 1
                If iLine = myView.MaxPageElements Then
                    myPage.PageChars = iChars
                    myView.Pages.Add(myPage)
                    iLine = 0
                    iChars = 0
                    ' Créer une nouvelle page s'il y a encore des éléments
                    If iCount < elts_page.Count Then
                        iPage += 1
                        PageIndex += 1
                        myPage = New PagePlayer

                        myPage.Id = iPage
                    End If
                End If
            Next
            If iLine <> 0 Then
                myPage.PageChars = iChars
                myView.Pages.Add(myPage)
            End If
            ' Calculer le temps des pages en fonction du nombre d'éléments affichés
            For Each pg In myView.Pages
                ' tenir compte du nombre de caractères aussi
                Dim time_elt = pg.Reservations.Count * time_by_elt
                Dim time_chars = pg.PageChars * time_by_chars
                pg.PageShowTime = IIf(time_elt > time_chars, time_elt, time_chars)
            Next
            ' Calculer le temps de la vue
            myView.ViewShowTime = myView.Pages.Sum(Function(e) e.PageShowTime)
            ' Ajouter la vue
            Views.Add(myView)
            iPage += 1
        Next
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/// Vue journaliere
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////
        Dim date_debut_jour = Now.Date
        Dim date_fin_jour = date_debut_jour.AddHours(24)
        Dim elts_page_day = (From e In db.RESERVATION.Include("LIEUX")
                       Where e.ETAT = EnumReservation.Confirmed And e.DATE_DEBUT >= date_debut_jour And e.DATE_DEBUT <= date_fin_jour And
                            Not black_list_hebdo.Contains(e.ID_LIEU)
                       Order By e.HEURE_DEBUT Ascending
                       Select e).ToList

        If elts_page_day.Count > 0 Then
            Dim param_day = (From p In db.PlanningSettings
                                    Where (p.PointAffichageId = _player_id Or p.PointAffichageId = 0) And
                                           p.ViewType = EnumViewType.Journalier
                                    Select p).FirstOrDefault

            If IsNothing(param_day) Then
                Exit Sub
            End If

            time_by_elt = param_day.ViewShowTime \ param_day.MaxPageElements
            time_by_chars = param_day.ViewShowTime \ param_day.MaxCharsPage
            PageIndex = 1
            iCount = 0
            Dim myViewDay As New ViewPlayer
            myViewDay.Id = iPage
            myViewDay.IdLieu = 0
            myViewDay.LibelleVue = param_day.LibelleVue
            myViewDay.ViewType = EnumViewType.Journalier
            myViewDay.LibellePeriode = String.Format(param_day.LibellePeriode, date_debut_jour.ToString("dddd dd/MM/yyyy"))
            myViewDay.AnimationTime = param_day.AnimationTime
            myViewDay.MaxPageElements = param_day.MaxPageElements

            If Not String.IsNullOrWhiteSpace(param_day.DivClassName) Then
                myViewDay.DivClassName = param_day.DivClassName '"StylePageConseil" pour la salle du conseil par exemple
            Else
                myViewDay.DivClassName = themes(iTheme Mod themesCount) : iTheme += 1 ' default_div_class_name
            End If

            If Not String.IsNullOrWhiteSpace(param_day.Animation) Then
                myViewDay.Animation = param_day.Animation
            Else
                myViewDay.Animation = transitions(iAnim Mod transitionsCount) : iAnim += 1
            End If

            iLine = 0 ' compte le nombre d'elts par page
            iCount = 0 ' compte le nombre d'elts par lieu ou salle
            iChars = 0
            Dim myPageDay As New PagePlayer

            For Each elt In elts_page_day
                If iChars > 0 AndAlso (iChars + elt.Objet.Length) >= param_day.MaxCharsPage Then
                    myPageDay.PageChars = iChars
                    myViewDay.Pages.Add(myPageDay)
                    iLine = 0
                    iChars = 0

                    iPage += 1
                    PageIndex += 1
                    myPageDay = New PagePlayer

                    myPageDay.Id = iPage
                End If

                ' Ajouter la réservation à la page
                myPageDay.Reservations.Add(elt)

                iChars += elt.Objet.Length

                iLine += 1
                iCount += 1
                If iLine = myViewDay.MaxPageElements Then
                    myPageDay.PageChars = iChars
                    myViewDay.Pages.Add(myPageDay)
                    iLine = 0
                    iChars = 0

                    ' Créer une nouvelle page s'il y a encore des éléments
                    If iCount < elts_page_day.Count Then
                        iPage += 1
                        PageIndex += 1
                        myPageDay = New PagePlayer

                        myPageDay.Id = iPage
                    End If
                End If
            Next
            If iLine <> 0 Then
                myPageDay.PageChars = iChars
                myViewDay.Pages.Add(myPageDay)
            End If

            ' Calculer le temps des pages en fonction du nombre d'éléments affichés
            For Each pg In myViewDay.Pages
                ' tenir compte du nombre de caractères aussi
                Dim time_elt = pg.Reservations.Count * time_by_elt
                Dim time_chars = pg.PageChars * time_by_chars
                pg.PageShowTime = IIf(time_elt > time_chars, time_elt, time_chars)
            Next
            ' Calculer le temps de la vue
            myViewDay.ViewShowTime = myViewDay.Pages.Sum(Function(e) e.PageShowTime)
            ' Ajouter la vue
            Views.Add(myViewDay)
        End If
    End Sub
End Class
