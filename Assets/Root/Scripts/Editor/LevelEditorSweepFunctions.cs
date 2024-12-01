using UnityEngine;
using UnityEditor;

namespace RollerBall
{
    public static class LevelEditorSweepFunctions
    {
        public const string Space = " ";
        public const string NoModifier = Const.MenuItem.NoModifier;
        public const string Control = Const.MenuItem.Control;
        public const string Shift = Const.MenuItem.Shift;
        public const string Alt = Const.MenuItem.Alt;

        public const string MenuFolder = Const.MenuItem.PlatformerKit + "Level Editor/";

        public const string SweepToFloorText = "Sweep To Floor";
        public const string SweepToCeilingText = "Sweep To Ceiling";
        public const string RayToFloorText = "Ray To Floor";
        public const string RayToCeilingText = "Ray To Ceiling";

        public const string SweepToFloorCmd     = SweepToFloorText      + Space + NoModifier + Const.MenuItem.END;
        public const string SweepToCeilingCmd   = SweepToCeilingText    + Space + NoModifier + Const.MenuItem.HOME;
        public const string RayToFloorCmd       = RayToFloorText        + Space + Shift + Const.MenuItem.END;
        public const string RayToCeilingCmd     = RayToCeilingText      + Space + Shift + Const.MenuItem.HOME;

        public const string SweepToFloorCmdArrows = SweepToFloorText + Space + Control + Const.MenuItem.DOWN;
        public const string SweepToCeilingCmdArrows = SweepToCeilingText + Space + Control + Const.MenuItem.UP;
        public const string RayToFloorCmdArrows = RayToFloorText + Space + Control + Alt + Const.MenuItem.DOWN;
        public const string RayToCeilingCmdArrows = RayToCeilingText + Space + Control + Alt + Const.MenuItem.UP;


        public static void SweepDirection(Vector3 direction, string undoMessage)
        {
            direction = direction.normalized;
            Undo.RecordObjects(Selection.transforms, undoMessage);

            foreach (Transform transform in Selection.transforms)
            {
                var collider = transform.GetComponent<Collider>();
                var hasCollider = collider != null;
                if (hasCollider)
                {
                    var rigidbody = transform.GetComponent<Rigidbody>();
                    var hasRigidbody = rigidbody != null;
                    var requiresRigidbody = !hasRigidbody;
                    if (requiresRigidbody)
                    {
                        rigidbody = transform.gameObject.AddComponent<Rigidbody>();
                    }

                    var hitObject = rigidbody.SweepTest(direction, out RaycastHit raycastInfo);
                    if (hitObject)
                    {
                        var positionDelta = direction * raycastInfo.distance;
                        transform.position += positionDelta;
                    }

                    if (requiresRigidbody)
                    {
                        Object.DestroyImmediate(rigidbody);
                    }
                }
            }
        }

        public static void RayDirection(Vector3 direction, string undoMessage)
        {
            Undo.RecordObjects(Selection.transforms, undoMessage);

            foreach (Transform transform in Selection.transforms)
            {
                var origin = transform.position;
                var ray = new Ray(origin, direction);
                var hitObject = Physics.Raycast(ray, out RaycastHit raycastInfo);
                if (hitObject)
                {
                    var positionDelta = direction * raycastInfo.distance;
                    transform.position += positionDelta;
                }
            }
        }

        #region Menu Items

        [MenuItem(MenuFolder + SweepToFloorCmd)]
        //[MenuItem(MenuFolder + SweepToFloorCmdArrows)]
        public static void SweepToFloor()
        {
            SweepDirection(Vector3.down, SweepToFloorText);
        }

        [MenuItem(MenuFolder + SweepToCeilingCmd)]
        //[MenuItem(MenuFolder + SweepToCeilingCmdArrows)]
        public static void SweepToCeiling()
        {
            SweepDirection(Vector3.up, SweepToFloorText);
        }

        [MenuItem(MenuFolder + RayToFloorCmd)]
        //[MenuItem(MenuFolder + RayToFloorCmdArrows)]
        public static void RayToFloor()
        {
            RayDirection(Vector3.down, RayToFloorText);
        }

        [MenuItem(MenuFolder + RayToCeilingCmd)]
        //[MenuItem(MenuFolder + RayToCeilingCmdArrows)]
        public static void RayToCeiling()
        {
            RayDirection(Vector3.up, RayToCeilingText);
        }

        #endregion

    }
}