
namespace Tasky.Droid.F

open System
open System.Collections.Generic
open System.Linq
open System.Text

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget

open Tasky.BL
open Tasky.BL.Managers

[<Activity (Label = "Task.F Details")>]
type TaskDetailsScreen() =
    inherit Activity()

    let mutable task = new Task()
    let mutable taskId = 0
    let mutable nameTextEdit : EditText = null
    let mutable notesTextEdit : EditText = null
    let mutable doneCheckbox : CheckBox = null

    member private this.SetupActionBar (?showUp) =
        let showUp = defaultArg showUp false
        this.ActionBar.SetDisplayHomeAsUpEnabled showUp

    override this.OnCreate(bundle) =
        base.OnCreate (bundle)
        // Create your application here

        this.SetupActionBar true
        this.SetContentView (Resource_Layout.TaskDetails)

        taskId <- this.Intent.GetIntExtra ("TaskID", 0)
        if taskId > 0 then
            task <- TaskManager.GetTask taskId

        nameTextEdit <- this.FindViewById<EditText> Resource_Id.txtName
        notesTextEdit <- this.FindViewById<EditText> Resource_Id.txtNotes
        doneCheckbox <- this.FindViewById<CheckBox> Resource_Id.chkDone

        if nameTextEdit <> null then
            nameTextEdit.Text <- task.Name
        if notesTextEdit <> null then
            notesTextEdit.Text <- task.Notes
        if doneCheckbox <> null then
            doneCheckbox.Checked <- task.Done


    override this.OnOptionsItemSelected (item) =
        if item.ItemId = Resource_Id.menu_save_task then
            this.Save ()
            true
        else if item.ItemId = Resource_Id.menu_delete_task then
            this.CancelDelete ()
            true
        else
            this.Finish ()
            base.OnOptionsItemSelected (item)

    override this.OnCreateOptionsMenu (menu) =
        this.MenuInflater.Inflate (Resource_Menu.menu_detailsscreen, menu)

        let menuItem = menu.FindItem(Resource_Id.menu_delete_task)

        if task.ID = 0 then
            menuItem.SetTitle "Cancel" |> ignore
        else
            menuItem.SetTitle "Delete" |> ignore

        base.OnCreateOptionsMenu (menu)


    member private this.Save () =
//        task <- { task with
//                    Name = nameTextEdit.Text
//                    Notes = notesTextEdit.Text
//                    Done = doneCheckbox.Checked }

        task.Name <- nameTextEdit.Text
        task.Notes <- notesTextEdit.Text
        task.Done <- doneCheckbox.Checked
        TaskManager.SaveTask task |> ignore
        this.Finish()

    member private this.CancelDelete() =
        if task.ID <> 0 then
            TaskManager.DeleteTask task.ID |> ignore
        this.Finish()