using UnityEditor;
using BlazeAISpace;

namespace BlazeAISpace 
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AlertStateBehaviour))]
    public class AlertStateBehaviourInspector : Editor
    {
        SerializedProperty moveSpeed,
        turnSpeed,
        idleAnim,
        moveAnim,
        animT,
        idleTime,
        playAudios,
        audioTime,
        returnToNormal,
        timeToReturnNormal,
        returningDuration,
        returningAnim,
        returningAnimT,
        playAudioOnReturn,
        avoidFacingObstacles,
        obstacleLayers,
        obstacleRayDistance,
        obstacleRayOffset,
        showObstacleRay;


        void OnEnable()
        {
            moveSpeed = serializedObject.FindProperty("moveSpeed");
            turnSpeed = serializedObject.FindProperty("turnSpeed");

            idleAnim = serializedObject.FindProperty("idleAnim");
            moveAnim = serializedObject.FindProperty("moveAnim");
            animT = serializedObject.FindProperty("animT");

            idleTime = serializedObject.FindProperty("idleTime");

            playAudios = serializedObject.FindProperty("playAudios");
            audioTime = serializedObject.FindProperty("audioTime");

            returnToNormal = serializedObject.FindProperty("returnToNormal");
            timeToReturnNormal = serializedObject.FindProperty("timeToReturnNormal");
            returningDuration = serializedObject.FindProperty("returningDuration");
            returningAnim = serializedObject.FindProperty("returningAnim");
            returningAnimT = serializedObject.FindProperty("returningAnimT");
            playAudioOnReturn = serializedObject.FindProperty("playAudioOnReturn");

            avoidFacingObstacles = serializedObject.FindProperty("avoidFacingObstacles");
            obstacleLayers = serializedObject.FindProperty("obstacleLayers");
            obstacleRayDistance = serializedObject.FindProperty("obstacleRayDistance");
            obstacleRayOffset = serializedObject.FindProperty("obstacleRayOffset");
            showObstacleRay = serializedObject.FindProperty("showObstacleRay");
        }

        public override void OnInspectorGUI () 
        {
            AlertStateBehaviour script = (AlertStateBehaviour) target;
            int spaceBetween = 20;
            
            EditorGUILayout.LabelField("SPEEDS", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(moveSpeed);
            EditorGUILayout.PropertyField(turnSpeed);

            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("ANIMATIONS", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(idleAnim);
            EditorGUILayout.PropertyField(moveAnim);
            EditorGUILayout.PropertyField(animT);

            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("IDLE", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(idleTime);
           
            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("AUDIOS", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(playAudios);
            if (script.playAudios) {
                EditorGUILayout.PropertyField(audioTime);
            }

            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("RETURN TO NORMAL", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(returnToNormal);
            if (script.returnToNormal) {
                EditorGUILayout.PropertyField(timeToReturnNormal);
                EditorGUILayout.PropertyField(returningDuration);
                EditorGUILayout.PropertyField(returningAnim);
                EditorGUILayout.PropertyField(returningAnimT);
                EditorGUILayout.PropertyField(playAudioOnReturn);
            }

            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("OBSTACLES", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(avoidFacingObstacles);
            if (script.avoidFacingObstacles) {
                EditorGUILayout.PropertyField(obstacleLayers);
                EditorGUILayout.PropertyField(obstacleRayDistance);
                EditorGUILayout.PropertyField(obstacleRayOffset);
                EditorGUILayout.PropertyField(showObstacleRay);
            }


            serializedObject.ApplyModifiedProperties();
        }
    }
}
