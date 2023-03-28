import { useEffect, useState } from "react"
import { fetchExercises, patchExercise, patchExerciseMusclegroups, patchExerciseSets, postExercise } from "../API/ExerciseAPI"
import ExerciseSelectionList from "../Components/Exercise/ExerciseSelectionList"
import { musclegroupCompare } from "../Util/SortHelper"
import Background from "../Images/backgrounds/hd-squad-color.jpeg";

const ExercisesOverview = props => {
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
    const saveExercise = event => {
        event.preventDefault()
        // console.log(event)
        if (event.nativeEvent.submitter.name === "save") { // PATCH workout
            if (state.selectedExercise !== null) {
                // console.log(event.target[0].value)
                const e = {
                    id: state.selectedExercise.id,
                    name: event.target[0].value,
                    description: event.target[1].value
                }
                if (e.name !== state.selectedExercise.name || e.description !== state.selectedExercise.description) patchExercise(state.selectedExercise.id, e)
                const ems = {
                    musclegroups: []
                }
                if (ems.musclegroups.toString() !== state.selectedExercise.musclegroups.toString()) patchExerciseMusclegroups(state.selectedExercise.id, ems)
                const ess = {
                    sets: []
                }
                if (ess.sets.toString() !== state.selectedExercise.sets.toString()) patchExerciseSets(state.selectedExercise.id, ess)

                const ne = {
                    id: e.id,
                    name: e.name,
                    description: e.description,
                    musclegroups: ems.musclegroups,
                    sets: ess.sets
                }
                const index = exercises.indexOf(state.selectedExercise)
                if (index > -1) {
                    exercises[index] = ne
                    setState({...state, selectedExercise: ne})
                }
                // const e = {
                //     id: state.selectedExercise.id,
                //     name: event.target[0].value,
                //     description: event.target[1].value,
                //     setIds: [],
                //     musclegroupIds: []
                // }
                // console.log(e)
                // patchExercise(state.selectedExercise.id, e)
                //     .then(r => {
                //         const index = exercises.indexOf(state.selectedExercise)
                //         if (index > -1) {
                //             exercises[index] = r
                //             setState({...state, selectedExercise: r})
                //         }
                //     })
            }
            else alert("Must select an exercise to save")
        }
        else { // POST workout
            const e = {
                name: event.target[0].value,
                description: event.target[1].value,
                setIds: [],
                musclegroupIds: []
            }
            postExercise(e)
                .then(r => {
                    setExercises([r, ...exercises])
                })
        }
    }

    return (
        <>
        <div class="bg">
            <img src={Background} alt=""/>
        </div>
        <div id="Exercises" className="d-flex flex-column align-items-center hpx-720 p-5 contentBox">
            <div className="d-flex flex-fill wp-100 min-h-0">
                <div className="d-flex flex-column text-center wp-100 exercises-item m-2 mb-0">
                    <div className="d-flex flex-column p-2 overflow-hidden">
                        Sort by: <button onClick={exercisesSort} className="btn btn-secondary border">Musclegroup</button>
                        <ExerciseSelectionList type="radio" exercises={exercises} exerciseSelected={exerciseSelected}/>
                    </div>
                </div>
                <div className="d-flex flex-column text-center wp-100 exercises-item m-2 mb-0 pt-2">
                    <h3>Details</h3>
                    <div className="d-flex flex-column justify-content-center hp-100">
                        {state.selectedExercise !== null &&
                        <>
                            <p>Exercise: {state.selectedExercise.name}</p>
                            <p>Description:</p>
                            <p>{state.selectedExercise.description}</p>
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
                    {props.contributor &&
                        <form onSubmit={saveExercise} key="EOForm-1" className="d-flex flex-column p-2">
                            <input type="text" defaultValue={state.selectedExercise?.name} placeholder="Exercise name" title="Letters and spaces only (between 2-20)" pattern="[A-Za-z\s]{2,20}" required/>
                            <input type="text" defaultValue={state.selectedExercise?.description} placeholder="Exercise description" title="Letters and spaces only (between 2-50)" pattern="[A-Za-z\s]{2,50}" required/>
                            <input type="submit" name="save" id="save-button-1" className="d-none"/>
                            <label htmlFor={"save-button-1"} className="btn btn-secondary border wp-100">Overwrite exercise</label>
                            <input type="submit" name="create" id="create-button-1" className="d-none"/>
                            <label htmlFor={"create-button-1"} className="btn btn-secondary border wp-100">Save as new exercise</label>
                        </form>
                    }
                </div>
            </div>
        </div>
        </>
    )
}

export default ExercisesOverview