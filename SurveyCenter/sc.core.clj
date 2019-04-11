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

; Obtiene la base de datos de evaluaciones de encuestas.
(defn survey-responses-db-get []
	(to-json (from-json (slurp "data/survey-responses.json"))))

; Guarda la base de datos de evaluaciones de encuestas.
(defn survey-responses-db-save [lib]
	(spit "data/survey-responses.json" lib))

; Crea una nueva encuesta en la base de datos de encuestas.
(defn survey-new [db id name]
	(to-json (conj (from-json db) {:id id :name name :items []})))

; Obtiene una encuesta por identificador.
(defn survey-get [id]
	(to-json (first (filter (fn [s] (= (:id s) id)) (from-json (survey-db-get))))))

; Agrega un item a una encuesta específica.
(defn survey-item-add [survey item]
	(to-json (apply vector(map (fn [y] (if (= (y :id) survey) (assoc y :items (conj (y :items) (from-json item)) )y)) (from-json (survey-db-get))))))

; Guarda una respuesta de una encuesta.
(defn survey-response-save [sr]
	(to-json (conj (from-json (survey-responses-db-get)) (from-json sr))))

; Obtiene las respuestas de una encuesta específica.
(defn survey-responses-get [response-id]
	(to-json (filter (fn [s] (= (:survey s) response-id)) (from-json (survey-responses-db-get)))))