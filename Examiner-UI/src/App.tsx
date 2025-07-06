import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import TagList from './pages/TagList';
import TagDetailPage from './pages/TagDetailPage';
import TagTimelinePage from './pages/TagTimelinePage';
import TagTimelineDetailPage from './pages/TagTimelineDetailPage';
import SystemLogsPage from './pages/SystemPages/ListLogsPage';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/tags" element={<TagList />} />
         <Route path="/tags/:id" element={<TagDetailPage />} />
         <Route path="/tags/:id/timeline" element={<TagTimelinePage />} />
         <Route path="/tags/:tagId/timeline/:timelineId" element={<TagTimelineDetailPage />} />
         <Route path="/system/logs" element={<SystemLogsPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
