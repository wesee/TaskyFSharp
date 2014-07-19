namespace Tasky.Droid.F

open System

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget

open Tasky.BL.Managers
open Tasky.BL
open System.Collections.Generic


type TaskListViewAdapter (context:Context, tasks:Task IList) =
    inherit BaseAdapter()

    member val Tasks = tasks
    override this.Count = this.Tasks.Count

    override this.GetItem position = new Java.Lang.String(this.Tasks.[position].ToString()) :> Java.Lang.Object


    override this.GetItemId position = int64 (this.Tasks.[position].GetHashCode())

    override this.GetView(position, convertView, parent) =
        let mutable convertView = convertView
        if convertView = null then
            let inflator = LayoutInflater.FromContext context
            convertView <- inflator.Inflate(Resource_Layout.TaskListItem, parent, false)

        let item = this.Tasks.[position]

        let nameLabel = convertView.FindViewById<TextView> Resource_Id.lblName
        nameLabel.Text <- item.Name
        let notesLabel = convertView.FindViewById<TextView> Resource_Id.lblDescription
        notesLabel.Text <- item.Notes

        let checkMark = convertView.FindViewById<ImageView> Resource_Id.checkMark
        if item.Done then
            checkMark.Visibility <- ViewStates.Visible
        else
            checkMark.Visibility <- ViewStates.Gone


        convertView


[<Activity (Label = "Tasky.F", MainLauncher = true)>]
type HomeScreen () =
    inherit Activity ()

    let mutable count:int = 1

    let mutable taskListView : ListView = null
    let mutable tasks : Task IList = null

    member private this.SetupActionBar (?showUp) =
        let showUp = defaultArg showUp false
        this.ActionBar.SetDisplayHomeAsUpEnabled showUp

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)

        // Set our view from the "main" layout resource
        this.SetContentView (Resource_Layout.HomeScreen)

        this.SetupActionBar false

        taskListView <- this.FindViewById<ListView> Resource_Id.lstTasks

        if taskListView <> null then
            taskListView.ItemClick.Add (fun (e : AdapterView.ItemClickEventArgs) ->
                let mutable taskDetails : Intent = new Intent (this, typeof<TaskDetailsScreen>)
                let task = tasks.Item e.Position 
                taskDetails.PutExtra ("TaskID", task.ID) |> ignore
                this.StartActivity taskDetails)

//        if this.ListAdapter = null then
//            this.ListAdapter <- new TaskListViewAdapter (this)



    override this.OnMenuItemSelected (featureId, item) =
        let itemId = item.ItemId
        let AddTaskItem = Resource_Id.menu_add_task
        if item.ItemId = AddTaskItem then   
            new Intent (this, typeof<TaskDetailsScreen>)
            |> this.StartActivity 
            |> ignore
            true
        else base.OnMenuItemSelected (featureId, item)


    override this.OnCreateOptionsMenu (menu) =
        this.MenuInflater.Inflate (Resource_Menu.menu_homescreen, menu)
        base.OnCreateOptionsMenu (menu)

    override this.OnResume () =
        base.OnResume ()

        tasks <- TaskManager.GetTasks ()

        let adapter = new TaskListViewAdapter (this, tasks)

        taskListView.Adapter <- adapter





