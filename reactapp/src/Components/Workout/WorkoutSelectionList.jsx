
const WorkoutSelectionList = props => {

    return (
        <div className="d-flex flex-column flex-fill align-items-center wp-100 hp-100 min-h-0 p-2">
            <p>Workouts:</p>
            {/* <GoalCreationContext.Consumer>
                {({workoutSelected}) => ( */}
                    <div className="d-flex flex-column flex-fill text-center overflow-y-scroll wp-100">
                        {props.workouts === null ? <div className="spinner-border align-self-center" role="status"/> :
                        props.workouts.map(workout => 
                            <div className="d-flex flex-column" key={workout.id}>
                                <input onChange={e => props.workoutSelected(e, workout)} type={props.type} name={"workout-list-" + props.k} id={`workout-${props.k}-${workout.id}`} className="btn-check"/>
                                <label htmlFor={`workout-${props.k}-${workout.id}`} className="btn btn-secondary border">{workout.name}</label>
                            </div>
                        )}
                        {/* <input onChange={e => workoutSelected(e, {id: 1, name: "Workout A"})} type="checkbox" name="workout-list-checkbox" id="workout-checkbox-1" className="btn-check"/>
                        <label htmlFor="workout-checkbox-1" className="btn btn-outline-secondary">Workout A</label>
                        <input onChange={e => workoutSelected(e, {id: 2, name: "Workout B"})} type="checkbox" name="workout-list-checkbox" id="workout-checkbox-2" className="btn-check"/>
                        <label htmlFor="workout-checkbox-2" className="btn btn-outline-secondary">Workout B</label> */}
                    </div>
                {/* )}
            </GoalCreationContext.Consumer> */}
        </div>
    )
}

export default WorkoutSelectionList