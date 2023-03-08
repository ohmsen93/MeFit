import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom'

import Dashboard from './Views/Dashboard';

function App() {
    return (
        <BrowserRouter>
            <div className="App">
                <Routes>
                    <Route path='/Dashboard' element={<Dashboard/>}></Route>
                </Routes>
            </div>
        </BrowserRouter>
    );
}

export default App;
