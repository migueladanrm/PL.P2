(ns surveycenter (:require [clojure.data.json :as json]))

; Funciones de conversión auxiliares.

(defn to-json [obj]
	(json/write-str obj))

(defn from-json [json]
	(json/read-str json :key-fn keyword))
;-------------------------------------------------

; Obtiene la base de datos de encuestas.
(defn survey-db-get []
	(to-json (from-json (slurp "data/survey-repo.json"))))

; Guarda la base de datos de encuestas.
(defn survey-db-save [lib]
	(spit "data/survey-repo.json" lib))

; Crea una nueva encuesta en la base de datos de encuestas.
(defn survey-new [db id name]
	(to-json (conj (from-json db) {:id id :name name :items []})))

; Obtiene una encuesta por identificador.
(defn survey-get [id]
	(to-json (first (filter (fn [s] (= (:id s) id)) (from-json (survey-db-get))))))

; Agrega un item a una encuesta específica.
(defn survey-item-add [survey item]
	(to-json (apply vector(map (fn [y] (if (= (y :id) survey) (assoc y :items (conj (y :items) (from-json item)) )y)) (from-json (survey-db-get))))))
