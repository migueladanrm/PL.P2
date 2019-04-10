(ns surveycenter (:require [clojure.data.json :as json]))

(defn to-json [obj]
	(json/write-str obj))

(defn from-json [json]
	(json/read-str json :key-fn keyword))
;-------------------------------------------------

(defn survey-library-get []
	(to-json (from-json (slurp "data/survey-repo.json"))))

(defn survey-library-save [lib]
	(spit "data/survey-repo.json" lib :append true))

(defn survey-new [library id name]
	(to-json (conj (from-json library) {:id id :name name :items []})))

(defn survey-get [db id]
	(to-json (first (filter (fn [s] (= (:id s) id)) (from-json db)))))

(defn survey-item-add [db survey item])
