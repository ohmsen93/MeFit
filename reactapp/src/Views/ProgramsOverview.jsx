import { useEffect, useState } from "react"
import { fetchPrograms, patchProgram, postProgram, patchProgramWorkouts, patchProgramCategories } from "../API/ProgramAPI"
import { fetchWorkouts } from "../API/WorkoutAPI"
import ProgramSelectionList from "../Components/Program/ProgramSelectionList"
import WorkoutSelectionList from "../Components/Workout/WorkoutSelectionList"
import { categoryCompare } from "../Util/SortHelper"
import Background from "../Images/backgrounds/hd-squad-color.jpeg";


const ProgramsOverview = props => {
    const [state, setState] = useState({
        selectedProgram: null,
        selectedPWorkout: null,
        selectedWorkout: null
    })
    const [programs, setPrograms] = useState(null)
    const [workouts, setWorkouts] = useState(null)
    const [pWorkouts, setPWorkouts] = useState([])

    useEffect(() => {
        // setPrograms("loading")
        const getPrograms = async () => {
            const ps = await fetchPrograms()
            console.log(ps)
            setPrograms(ps.reverse())
        }
        getPrograms()
    }, [])
    useEffect(() => {
        // setWorkouts("loading")
        const getWorkouts = async () => {
            const ws = await fetchWorkouts()
            console.log(ws)
            setWorkouts(ws.reverse())
        }
        getWorkouts()
    }, [])

    const programSelected = (event, program) => {
        console.log(program)
        if (event.target.checked) {
            setState({...state, selectedProgram: program, selectedPWorkout: null})
            setPWorkouts(workouts?.filter(w => program.workouts?.includes(w.id)) || [])
        }
        else setState({...state, selectedProgram: null})
    }
    const workoutSelected = (event, workout) => {
        console.log(workout)
        if (event.target.checked) setState({...state, selectedWorkout: workout})
        else setState({...state, selectedWorkout: null})
    }
    const pWorkoutSelected = (event, workout) => {
        console.log(workout)
        if (event.target.checked) setState({...state, selectedPWorkout: workout})
        else setState({...state, selectedPWorkout: null})
    }
    const programsSort = () => {
        const ps = [...programs].sort(categoryCompare)
        setPrograms(ps)
    }
    const addPWorkout = () => {
        if (state.selectedWorkout !== null) {
            const index = pWorkouts.indexOf(state.selectedWorkout)
            if (index === -1) // Currently only add if unique
                setPWorkouts([...pWorkouts, state.selectedWorkout])
            else alert("Workout is already in program")
        }
        else alert("Must select a registered workout to add")
    }
    const removePWorkout = () => {
        if (state.selectedPWorkout !== null) {
            const pws = [...pWorkouts]
            const index = pws.indexOf(state.selectedPWorkout)
            if (index > -1) {
                pws.splice(index, 1)
                setPWorkouts(pws)
                setState({...state, selectedPWorkout: null})
            }
            else console.log("ERROR: PWORKOUT NOT FOUND")
        }
        else alert("Must select a program workout to remove")
    }
    const saveProgram = event => {
        event.preventDefault()
        // console.log(event)
        if (pWorkouts.length > 0) {
            if (event.nativeEvent.submitter.name === "save") { // PATCH program
                if (state.selectedProgram !== null) {
                    // console.log(event.target[0].value)
                    const p = {
                        id: state.selectedProgram.id,
                        name: event.target[0].value
                    }
                    if (p.name !== state.selectedProgram.name) patchProgram(state.selectedProgram.id, p)
                    const pws = {
                        workouts: pWorkouts.map(w => w.id)
                    }
                    if (pws.workouts.toString() !== state.selectedProgram.workouts.toString()) patchProgramWorkouts(state.selectedProgram.id, pws)
                    const pcs = {
                        categories: state.selectedProgram.categories
                    }
                    if (pcs.categories.toString() !== state.selectedProgram.categories.toString()) patchProgramCategories(state.selectedProgram.id, pcs)

                    const np = {
                        id: p.id,
                        name: p.name,
                        workouts: pws.workouts,
                        categories: pcs.categories
                    }
                    const index = programs.indexOf(state.selectedProgram)
                    if (index > -1) {
                        programs[index] = np
                        setState({...state, selectedProgram: np})
                    }
                    //patchProgram(state.selectedProgram.id, p)
                        // .then(r => {
                        //     const index = programs.indexOf(state.selectedProgram)
                        //     if (index > -1) {
                        //         programs[index] = r
                        //         setState({...state, selectedProgram: r})
                        //     }
                        // })
                }
                else alert("Must select a program to save")
            }
            else { // POST program
                const p = {
                    name: event.target[0].value,
                    workoutIds: pWorkouts.map(w => w.id),
                    categoryIds: []
                }
                postProgram(p)
                    .then(r => {
                        console.log(r)
                        setPrograms([r, ...programs])
                    })
            }
        }
        else alert("Must add at least 1 workout to program")
    }

    return (
        <>
        <div class="bg">
            <img src={Background} alt=""/>
        </div>
        <div id="Programs" className="d-flex flex-column align-items-center hpx-720 p-5 contentBox">
            <div className="d-flex flex-fill wp-100 min-h-0">
                <div className="d-flex flex-column text-center wp-100 programs-item m-2 mb-0">
                    <div className="d-flex flex-column p-2 overflow-hidden">
                        Sort by: <button onClick={programsSort} className="btn btn-secondary border mt-2">Category</button>
                        <ProgramSelectionList type="radio" programs={programs} programSelected={programSelected}/>
                    </div>
                </div>
                <div className="d-flex flex-column text-center wp-100 programs-item mt-2 mb-0 pt-2">
                    <WorkoutSelectionList type="radio" workouts={pWorkouts} workoutSelected={pWorkoutSelected} k={1}/>
                    {props.contributor && 
                        // <div class="overflow-y-scroll">
                        <>
                            <div className="d-flex p-2">
                                <button onClick={removePWorkout} className="btn btn-secondary border wp-100">↓</button>
                                <button onClick={addPWorkout} className="btn btn-secondary border wp-100">↑</button>
                            </div>
                            <WorkoutSelectionList type="radio" workouts={workouts} workoutSelected={workoutSelected} k={2}/>
                        </>
                        // </div>
                    }
                </div>

                <div className="d-flex flex-column text-center wp-100 programs-item m-2 mb-0 pt-2">
                    <h3>Details</h3>
                    <div className="d-flex flex-column justify-content-center hp-100">
                        {state.selectedProgram !== null &&
                        <>
                            <p>Program: {state.selectedProgram.name}</p>

                            {state.selectedProgram.categories?.length > 0 ?
                            <>
                            <p>Categories:</p>
                            {state.selectedProgram.categories.map(cg => 
                                <p key={cg.id}>{cg.name}</p>
                            )}
                            </>
                            : <p>No categories</p>
                            }
                        </>
                        }
                    </div>
                    <div className="d-flex flex-column justify-content-center hp-100">
                        {state.selectedPWorkout !== null &&
                        <>
                            <p>Workout: {state.selectedPWorkout.name}</p>
                            <p>Type: {state.selectedPWorkout.type}</p>
                        </>
                        }
                    </div>
                    {props.contributor &&
                        <form onSubmit={saveProgram} key="POForm-1" className="d-flex flex-column p-2">
                            <input type="text" defaultValue={state.selectedProgram?.name} placeholder="Program name" title="Letters and spaces only (between 2-40)" pattern="[A-Za-z\s]{2,40}" required/>
                            <input type="submit" name="save" id="save-button-1" className="d-none"/>
                            <label htmlFor={"save-button-1"} className="btn btn-secondary border wp-100">Overwrite program</label>
                            <input type="submit" name="create" id="create-button-1" className="d-none"/>
                            <label htmlFor={"create-button-1"} className="btn btn-secondary border wp-100">Save as new program</label>
                        </form>
                    }
                </div>
            </div>
        </div>
        </>
    )
}

export default ProgramsOverview