(ns surveycenter (:require [clojure.data.json :as json]))

(defn to-json [obj]
	(json/write-str obj))

(defn from-json [json]
	(json/read-str json :key-fn keyword))
;-------------------------------------------------

(defn survey-new [library id name]
	(json/write-str (conj (json/read-str library) {:id id :name name :items []})))