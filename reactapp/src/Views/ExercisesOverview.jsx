import { useEffect, useState } from "react"
import { fetchExercises } from "../API/ExerciseAPI"
import ExerciseSelectionList from "../Components/Exercise/ExerciseSelectionList"
import { musclegroupCompare } from "../Util/SortHelper"

const ExercisesOverview = () => {
    const [state, setState] = useState({
        selectedExercise: null
    })
    const [exercises, setExercises] = useState(null)

    useEffect(() => {
        // setExercises("loading")
        const getExercises = async () => {
            const es = await fetchExercises()
            console.log(es)
            setExercises(es.reverse())
        }
        getExercises()
    }, [])

    const exerciseSelected = (event, exercise) => {
        console.log(exercise)
        if (event.target.checked) setState({...state, selectedExercise: exercise})
        else setState({...state, selectedExercise: null})
    }
    const exercisesSort = () => {
        const es = [...exercises].sort(musclegroupCompare)
        setExercises(es)
    }

    return (
        <>
        <div className="d-flex flex-column align-items-center hpx-720 p-5">
            <div className="d-flex flex-fill border wp-100 min-h-0">
                <div className="d-flex flex-column text-center wp-100">
                    Sort by: <button onClick={exercisesSort} className="btn btn-outline-secondary">Musclegroup</button>
                    <ExerciseSelectionList type="radio" exercises={exercises} exerciseSelected={exerciseSelected}/>
                </div>
                <div className="d-flex flex-column text-center wp-100">
                    <h3>Details</h3>
                    <div className="d-flex flex-column justify-content-center hp-100">
                        {state.selectedExercise !== null &&
                        <>
                            <p>Exercise: {state.selectedExercise.name}</p>
                            {state.selectedExercise.musclegroups?.length > 0 ?
                            <>
                            <p>Musclegroups:</p>
                            {state.selectedExercise.musclegroups?.map(mg => 
                                <p key={mg.id}>{mg.name}</p>
                            )}
                            </>
                            : <p>No musclegroups</p>
                            }
                        </>
                        }
                    </div>
                </div>
            </div>
        </div>
        </>
    )
}

export default ExercisesOverview