import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import TagList from './pages/TagList';
import TagDetailPage from './pages/TagDetailPage';
import TagTimelinePage from './pages/TagTimelinePage';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/tags" element={<TagList />} />
         <Route path="/tags/:id" element={<TagDetailPage />} />
         <Route path="/tags/:id/timeline" element={<TagTimelinePage />} />

      </Routes>
    </BrowserRouter>
  );
}

export default App;
