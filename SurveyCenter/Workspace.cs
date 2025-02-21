﻿using clojure.lang;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using static System.Console;

namespace SurveyCenter
{
    /// <summary>
    /// Capa de interoperabilidad Clojure/.NET Framework.
    /// </summary>
    public static class Workspace
    {
        public const string CORE_NS = "surveycenter";
        public const string PATH_DATA = "data";
        public static readonly string PATH_SURVEY_REPO = $"{PATH_DATA}\\survey-repo.json";
        public static readonly string PATH_SURVEY_RESPONSES = $"{PATH_DATA}\\survey-responses.json";

        public static string GenerateHexId()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }

        public static void SetupWorkspace()
        {
            if (!Directory.Exists(PATH_DATA))
                Directory.CreateDirectory(PATH_DATA);

            if (!File.Exists(PATH_SURVEY_REPO))
                File.WriteAllText(PATH_SURVEY_REPO, "[]");

            if (!File.Exists(PATH_SURVEY_RESPONSES))
                File.WriteAllText(PATH_SURVEY_RESPONSES, "[]");
        }

        public static void InitializeClojureRuntime()
        {
            WriteLine("Initializing Clojure runtime...");

            RT.load("json");
            RT.load("sc.core");

            WriteLine("Clojure runtime loaded!");
        }

        public static void SurveyLibrarySave(JArray payload)
        {
            File.WriteAllText(PATH_SURVEY_REPO, payload.ToString());
        }

        public static void SurveyResponsesSave(JArray payload)
        {
            File.WriteAllText(PATH_SURVEY_RESPONSES, payload.ToString());
        }

        public static JArray SurveyLibraryGet()
        {
            var surveyLibraryGet = RT.var("surveycenter", "survey-db-get");
            var library = JArray.Parse(Convert.ToString(surveyLibraryGet.invoke()));

            return library;
        }

        public static JObject SurveyItemAdd(string surveyId, JObject surveyItem)
        {
            var itemAdd = RT.var(CORE_NS, "survey-item-add");
            var result = Convert.ToString(itemAdd.invoke(surveyId, surveyItem.ToString()));
            WriteLine(Convert.ToString(result));

            SurveyLibrarySave(JArray.Parse(result));

            return surveyItem;
        }

        public static JObject SurveyGet(string surveyId)
        {
            var surveyGet = RT.var(CORE_NS, "survey-get");
            var result = surveyGet.invoke(surveyId);

            try {
                return JObject.Parse(result.ToString());
            } catch {
                return null;
            }
        }

        public static string SurveyNew(string name)
        {
            var id = GenerateHexId();
            var surveyNew = RT.var(CORE_NS, "survey-new");
            var result = surveyNew.invoke(SurveyLibraryGet().ToString(), id, name);
            SurveyLibrarySave(JArray.Parse(result.ToString()));

            WriteLine($"Se ha creado una encuesta con el identificador '{id}'.");

            return id;
        }

        public static void SurveyResponseSave(JObject surveyResponse)
        {
            var surveyResponseSave = RT.var(CORE_NS, "survey-response-save");
            var data = surveyResponseSave.invoke(surveyResponse.ToString());

            SurveyResponsesSave(JArray.Parse(data.ToString()));
        }

        public static JArray SurveyResponsesGet(string id)
        {
            var surveyResponseSave = RT.var(CORE_NS, "survey-responses-get");
            var data = surveyResponseSave.invoke(id);

            return JArray.Parse(data.ToString());
        }
    }
}